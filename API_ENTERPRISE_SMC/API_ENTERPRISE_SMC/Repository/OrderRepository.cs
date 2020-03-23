using API_ENTERPRISE_SMC.Data;
using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TodoContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderRepository(TodoContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._config = config;
        }

        public async Task<QueryResult<ResponsOrder>> CreateOrderAsync(List<RequestOrder> ordenes)
        {
            var result = new QueryResult<ResponsOrder>();
            List<ResponsOrder> listObjRes = new List<ResponsOrder>();
            var handler = new JwtSecurityTokenHandler();
            String accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            accessToken = accessToken.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(accessToken);
            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthUser>(tokenS.Claims.First(claim => claim.Type == "UserData").Value);
            


            //Se realiza la operacion por orden
            foreach (RequestOrder item in ordenes)
            {
                ResponsOrder objRes = new ResponsOrder();
                //Se obtienen los demograficos de la historia
                List<DemoConfig> demoConfigsHistoria = new List<DemoConfig>();
                demoConfigsHistoria = this._config.GetSection("Demograficos:Historia").Get<List<DemoConfig>>();
                //DEMOGRAFICOS DE LA HISTORIA
                String hisDem = "";
                String hisDemCod = "";
                String hisDemNoCod = "";
                String TD = "";
                foreach (RequestDemographic itemDeReq in item.patient.demographics)
                {
                    foreach (DemoConfig itemD in demoConfigsHistoria)
                    {
                        //si el codigo del demografico no es vacio es porque esta en uso en la integracion
                        //se valida que el id que llega de la integracion sea el mismo que esta configurado en el config
                        if (itemD.Code != "" && itemD.Code==itemDeReq.id) {
                            hisDem += itemD.Id + "|";
                            if (itemD.IsCoded) {
                                RequestDemographic objAux = new RequestDemographic();
                                objAux = await getDemographicItemIdAsync(itemDeReq);
                                if (objAux != null)
                                {
                                    hisDemCod += objAux.idDemographicItem + "|";
                                    hisDemNoCod += "NULL|";
                                }
                                else {
                                    hisDemCod += "NULL|";
                                    hisDemNoCod += "NULL|";
                                }
                                
                            }
                            else
                            {
                                hisDemCod += "NULL|";
                                hisDemNoCod += itemDeReq.value + "|";
                            }
                            
                        }
                    }

                    if (itemDeReq.id == "TD")
                    {
                        TD = itemDeReq.value;
                    }
                }

                //Se verifica si la historia existe o no
                //si existe se actualiza si no se crea
                var historia = TD + item.patient.identification;
                var validar = GetHistorybyCode(historia);
                var idHistoria = validar.Count > 0 ? validar.ElementAt(0).id: -1;
                long unixDate = item.patient.birthDay;
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime dateBirthDay = start.AddMilliseconds(unixDate).ToLocalTime();
                if (idHistoria > 0)
                {
                    //se actualiza        
                    try
                    {
                        UpdateHistoria(historia, item.patient.names, item.patient.lastName, Convert.ToInt32(item.patient.sex), dateBirthDay, user.id, hisDem, hisDemNoCod, hisDemCod, idHistoria, "", 0);
                    }
                    catch (Exception e)
                    {
                        return null;
                    }


                }
                else {
                    //se crea la historia        
                    try
                    {
                        idHistoria = SetHistoria(historia, item.patient.names, item.patient.lastName, Convert.ToInt32(item.patient.sex), dateBirthDay, user.id, hisDem, hisDemNoCod, hisDemCod, "");
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }

                //DEMOGRAFICOS DE LA ORDEN
                String ordDem = "";
                String ordDemCod = "";
                String ordDemNoCod = "";
                String OR = "";
                String H = "";
                Int32 idDemoH = 0;
                Int32 idDemo = 0;
                String Tests = this._config.GetValue<string>("IdExamen") + "|";
                String TestsRate = "NULL|NULL|";
                int countRe = 0;
                int countRef = 0;
                int countOr = 0;
                int countH = 0;
                //Se obtienen los demograficos de la orden
                List<DemoConfig> demoConfigsOrder = new List<DemoConfig>();
                demoConfigsOrder = this._config.GetSection("Demograficos:Orden").Get<List<DemoConfig>>();
                foreach (RequestDemographic itemDeReq in item.demographics)
                {
                    foreach (DemoConfig itemD in demoConfigsOrder)
                    {
                        //si el codigo del demografico no es vacio es porque esta en uso en la integracion
                        //se valida que el id que llega de la integracion sea el mismo que esta configurado en el config
                        if (itemD.Code != "" && itemD.Code == itemDeReq.id)
                        {
                            ordDem += itemD.Id + "|";
                            if (itemD.IsCoded)
                            {
                                RequestDemographic objAux = new RequestDemographic();
                                objAux = await getDemographicItemIdAsync(itemDeReq);
                                if (objAux!=null) {
                                    ordDemCod += objAux.idDemographicItem + "|";
                                    ordDemNoCod += "NULL|";
                                }else {
                                    ordDemCod += "NULL|";
                                    ordDemNoCod += "NULL|";
                                }
                            }
                            else
                            {
                                ordDemCod += "NULL|";
                                ordDemNoCod += itemDeReq.value + "|";
                            }
                        }

                        if (itemD.Code == "RE" && countRe==0)
                        {
                            ordDem += itemD.Id + "|";
                            RequestDemographic objAux = new RequestDemographic();
                            objAux.id = itemD.Code;
                            objAux.demographic = itemD.Name;
                            objAux.value = item.result.result;
                            objAux = await getDemographicItemIdAsync(objAux);
                            if (objAux != null)
                            {
                                ordDemCod += objAux.idDemographicItem + "|";
                                ordDemNoCod += "NULL|";
                            }
                            else
                            {
                                ordDemCod += "NULL|";
                                ordDemNoCod += "NULL|";
                            }
                            countRe++;
                        }

                        if (itemD.Code == "FRE" && countRef==0)
                        {
                            ordDem += itemD.Id + "|";
                            long unixDateRE = item.result.validation;
                            DateTime startRE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            DateTime dateRE = start.AddMilliseconds(unixDateRE).ToLocalTime();
                            
                            ordDemCod += "NULL|";
                            ordDemNoCod += dateRE.ToString("yyyyMMdd hh:mm:ss") + "|";
                            countRef++;
                        }

                        if (itemD.Code == "OR" && countOr==0)
                        {
                            ordDem += itemD.Id + "|";
                            OR = item.order;
                            idDemo = itemD.Id;
                            ordDemCod += "NULL|";
                            ordDemNoCod += item.order + "|";
                            countOr++;
                        }
                    }
                }

                //Se verifica si la orden existe o no
                //si existe se retorna true
                //Se buscara la orden que tenga el demografico
                //var orderLab20 = await GetOrderbyORAsync(OR, idDemo);
                //if (orderLab20 != null)
                //{
                    //Retorna true si ya existia esa orden del HIS
                    //objRes.order = OR;
                    //objRes.success = true;
                //}
                //else {
                    //Si no existia la Orden del HIS se crea
                    long OrderLIS = 0;
                    try
                    {
                        OrderLIS = SetOrder(0, Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")), user.id, idHistoria, "R", ordDem, ordDemNoCod, ordDemCod, Tests, TestsRate, -1, "", "", 0);
                    }
                    catch (Exception e)
                    {
                        OrderLIS = 0;
                    }
                    objRes.order = OR;
                    objRes.success = OrderLIS > 0 ? true : false;
                //}

                listObjRes.Add(objRes);
            }

            result.Items = listObjRes;
            return result;
        }

        public async Task<RequestDemographic> getDemographicItemIdAsync(RequestDemographic demo)
        {
            var result = new QueryResult<RequestDemographic>();

            var demoIt = (from It in this._context.DemoIt
                          where It.codigo == demo.value
                              select new RequestDemographic
                              {
                                  id = demo.id,
                                  demographic = demo.demographic,
                                  value = demo.value,
                                  idDemographicItem = It.id.ToString(),                              
                              }
                          );

            result.Items = await demoIt.ToListAsync();
            return result.Items.Count() > 0 ? result.Items.ElementAt(0): null;
        }

         public async Task<Lab20> GetOrderbyORAsync(String OR, Int32 idDemo)
         {
            var result = new QueryResult<Lab20>();
            try
            {
                var lab20 = (from Dem in this._context.Lab20
                          where Dem.idDemographic == idDemo && Dem.Dato.Equals(OR)
                          select new Lab20
                          {
                              idOrder = Dem.idOrder,
                              idDemographic = Dem.idDemographic,
                              Dato = Dem.Dato,
                              idDemographicItem = Dem.idDemographicItem
                          }
                         );
                result.Items = await lab20.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
            return result.Items.Count() > 0 ? result.Items.ElementAt(0) : null;
        }

        public List<Lab21> GetHistorybyCode(String History)
        {
            string query = "SELECT Lab21C1, Lab21C2 FROM Lab21 WHERE dbo.LabFn01(Lab21C2,'104F') = '" + History + "'";
            var cont = _context.Lab21.FromSql(query).ToList();

            return cont;
        }

        public bool UpdateHistoria(string codhistoria, string nombres, string apellidos, int sexo, DateTime fechanacimiento, Int32 user, string iddemo, string valuesdemo, string valuesCodDemo, Int32 ID_historia, String comment, Int64 order)
        {
            //imagen fija de la historia
            string query = "exec Lab21Lab19_Upd @lab21c2='" + codhistoria.ToUpper() + "', @lab21c3 = '', @lab21c4 = '" + nombres + "', @lab21c5= '" + apellidos + "', @lab21c6= '" + sexo + "', @lab21c7 = '',  @lab21c8 = '', @lab21c10= '" + fechanacimiento.ToString("dd-MM-yyyy hh:mm:ss") + "', @lab04c1 = '" + user + "', @Demos = '" + iddemo + "', @val1 = '" + valuesdemo + "', @val2 = '" + valuesCodDemo + "', @lab21c1 = '" + ID_historia + "', @Lab28C2 = '"+ comment + "', @Lab22C1 = '"+ order + "'; SELECT TOP(1) Lab21C1, Lab21C2 FROM Lab21;";
            var cont = _context.Lab21.FromSql(query).ToList();
            return true;
        }

        public Int64 SetOrder(Int64 order, int date, int user, Int32 history, string orderType, string idDem, string valuesDem, string valuesCodDemo, string lab39c1, string lab63c1, int lab63c1lab22, string price, string lab60, Int64 recallOrder)
        {
            //imagen fija de la orden
            string separator = ".";
            string sede = "";
            if (lab63c1lab22 == -1)
            {
                sede = "NULL";
            }
            else
            {
                sede = "'"+lab63c1lab22+"'";
            }
            string query = "declare @p16 bigint; set @p16=0; exec Lab22Lab20_Set @Lab22C1="+ order + ", @Lab22C3="+ date + ", @Lab04C1="+ user +", @Lab21C1="+ history + ", @Lab22C12='"+ orderType + "', @Demos='"+ idDem + "', @val1='"+ valuesDem + "', @val2='"+ valuesCodDemo + "', @lab39c1='"+ lab39c1 + "', @lab63c1='"+ lab63c1 + "', @precio='"+ price + "', @lab60=NULL, @lab63c1lab22="+ sede +", @LabSEP='"+ separator +"', @Lab22C17=0, @Contador=@p16 output; SELECT TOP(1) @p16 AS Lab22C1 FROM Lab22;";
            var cont = _context.Lab22.FromSql(query).ToList();
            return cont.Count > 0 ? cont.ElementAt(0).id : 0;
        }

        public Int32 SetHistoria(string codhistoria, string nombres, string apellidos, int sexo, DateTime fechanacimiento, Int32 user, string iddemo, string valuesdemo, string valuesCodDemo, String comment)
        {
            //imagen fija de la historia
            string query = "declare @p13 int; set @p13=9; exec Lab21Lab19_Set @lab21c2='" + codhistoria.ToUpper() + "', @lab21c3 = '', @lab21c4 = '" + nombres + "', @lab21c5= '" + apellidos + "', @lab21c6= '" + sexo + "', @lab21c7 = '',  @lab21c8 = '', @lab21c10='" + fechanacimiento.ToString("dd-MM-yyyy hh:mm:ss") + "', @lab04c1 = '" + user + "', @Demos = '" + iddemo + "', @val1 = '" + valuesdemo + "', @val2 = '" + valuesCodDemo + "', @lab21c1=@p13 output, @Lab28C2 = '" + comment + "'; SELECT TOP(1) @p13 as Lab21C1, '' as Lab21C2 FROM Lab21;";
            var cont = _context.Lab21.FromSql(query).ToList();
            return cont.Count > 0 ? cont.ElementAt(0).id : 0;
        }
    }
}
