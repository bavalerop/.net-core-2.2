using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ENTERPRISE.Data;
using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_ENTERPRISE.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TodoContext _context;
        private readonly RijndaelAlgorithm _objSecurity;
        private readonly ConfiguracionRepository _objConf;

        public AuthRepository(TodoContext context)
        {
            this._context = context;
            this._objSecurity = new RijndaelAlgorithm();
            this._objConf = new ConfiguracionRepository(this._context);
        }
        public async Task<QueryResult<AuthUser>> GetAccess(RequestAuthUser uss)
        {

            var result = new QueryResult<AuthUser>();
            var resultLab93 = new QueryResult<UsuarioDemograficoItem>();

            var sede = false;

            try
            {
                //Se valida si el usuario es administrador para cambiar el estado
                var users = (from d in this._context.AuthUser
                             join ur in this._context.userXrol on d.id equals ur.idUser
                             join r in this._context.rol on ur.idRol equals r.id
                             where d.userName == uss.user && ur.estado == 1 && r.estado == 1
                             select new AuthUser
                             {
                                 id = d.id,
                                 userName = d.userName,
                                 lastName = d.lastName,
                                 name = d.name,
                                 branch = uss.branch,
                                 photo = "",
                                 administrator = r.admin == 1 ? true : false,
                                 acceso = false,
                                 rol = r.rol,
                                 password = d.password,
                                 valorSalt = d.valorSalt,
                                 valorIV = d.valorIV
                             }

                          );
                //Se verifica si trabaja por sedes para hacer la validacion
                var TrabajoPorSede = await this._objConf.GetJobBranch();

                if (TrabajoPorSede.Items.ElementAt(0).value == "True")
                {
                    var DemoSede = await this._objConf.GetConfig("DemograficoSede");
                    //Consulta las sedes validas para ese usuario
                    var UsXDeXIt = (from It in this._context.DemoIt
                                    join dg in this._context.Demogra on It.idDemografico equals dg.id
                                    join Udi in this._context.uDIt on It.id equals Udi.idDemograficoItem
                                    join Us in this._context.AuthUser on Udi.idUser equals Us.id
                                    where dg.id == Convert.ToInt16(DemoSede.Items.ElementAt(0).value) && dg.estado == 1 && Udi.estado == 1 && Us.userName == uss.user
                                    select new UsuarioDemograficoItem
                                    {
                                        idDemograficoItem = Udi.idDemograficoItem,
                                        idUser = Udi.idUser,
                                        estado = Udi.estado
                                    }

                              );
                    resultLab93.Items = await UsXDeXIt.ToListAsync();

                    for (int i = 0; i < resultLab93.Items.Count(); i++)
                    {
                        if (resultLab93.Items.ElementAt(i).idDemograficoItem == uss.branch)
                        {
                            sede = true;
                        }
                    }
                }
                else
                {
                    sede = true;
                }
                //Alamacena una lista de users en el objeto QueryResult
                result.Items = await users.ToListAsync();
                //Desencripta la contraseña del usuario y la almacena denuevo en el objeto user desencriptada
                result.Items.ElementAt(0).password = this._objSecurity.Desencripta(result.Items.ElementAt(0).password, result.Items.ElementAt(0).valorSalt, result.Items.ElementAt(0).valorIV);

                //Si la contraseña es correcta (Tiene acceso)
                if (result.Items.ElementAt(0).password == uss.password && sede == true)
                {
                    result.Items.ElementAt(0).acceso = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Se retorna la lista
            return result;
        }

        public async Task<QueryResult<ResponsBranch>> GetBranches()
        {
            var result = new QueryResult<ResponsBranch>();

            try
            {
                var TrabajoPorSede = await this._objConf.GetJobBranch();

                if (TrabajoPorSede.Items.ElementAt(0).value == "True")
                {
                    var DemoSede = await this._objConf.GetConfig("DemograficoSede");

                    var sed = (from It in this._context.DemoIt
                               where It.idDemografico == Convert.ToInt16(DemoSede.Items.ElementAt(0).value)
                               select new ResponsBranch
                                 {
                                     id = Convert.ToInt16(It.id),
                                     code = It.codigo,
                                     abbreviation = "",
                                     name = It.demograficoItem.Trim(),
                                     state = It.estado == 1 ? true : false
                               }

                              );
                    //Alamacena una lista de sedes en el objeto QueryResult
                    result.Items = await sed.ToListAsync();
                    //Se retorna la lista
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Se retorna la lista
            return result;
        }

    }
}
