using API_ENTERPRISE.Data;
using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoContext _context;
        private readonly ConfiguracionRepository _objConf;

        public UserRepository(TodoContext context)
        {
            this._context = context;
            this._objConf = new ConfiguracionRepository(this._context);
        }

        public async Task<QueryResult<ResponsUser>> GetUser()
        {
            var result = new QueryResult<ResponsUser>();

            try
            {
                //Se obtienes datos de usuario
                var usersRespons = (from d in this._context.AuthUser
                             join ur in this._context.userXrol on d.id equals ur.idUser
                             where ur.estado == 1 
                             select new ResponsUser
                             {
                                 id = d.id,
                                 name = d.name,
                                 lastName = d.lastName,
                                 userName = d.userName,
                                 state = d.estado == 1 ? true : false,
                                 identificacion = "",
                                 email = d.mail,
                                 photo = ""  
                             }

                          );

                result.Items = await usersRespons.ToListAsync();

                //Se recorren los usuarios con el fin de buscar sus sedes y areas

                //Areas
                foreach (ResponsUser item in result.Items) {
                    //Se buscan las areas por usuario
                    var areas = (from usec in this._context.UsSection
                                 join sec in this._context.section on usec.idSection equals sec.id
                                 where usec.idUser == item.id && usec.state == 1
                                 select new SectionUser
                                 {
                                     access = true,
                                     area = new ResponsSection {
                                         id = sec.id,
                                         abbreviation = sec.abbreviation,
                                         name = sec.name,
                                         state = sec.estado == 1 ? true : false
                                        }
                                    }

                              );
                    item.areas = await areas.ToListAsync();
                    //Se buscan las sedes por usuario
                    //Consulta las sedes validas para ese usuario
                    var DemoSede = await this._objConf.GetConfig("DemograficoSede");

                    var sedes = (from It in this._context.DemoIt
                                    join dg in this._context.Demogra on It.idDemografico equals dg.id
                                    join Udi in this._context.uDIt on It.id equals Udi.idDemograficoItem
                                    where dg.id == Convert.ToInt16(DemoSede.Items.ElementAt(0).value) && dg.estado == 1 && Udi.estado == 1 && Udi.idUser == item.id
                                    select new BranchUser
                                    {
                                        access = true,
                                        branch = new ResponsBranch
                                        {
                                            id = Convert.ToInt16(It.id),
                                            code = It.codigo,
                                            abbreviation = "",
                                            name = It.demograficoItem.Trim(),
                                            state = It.estado == 1 ? true : false
                                        }
                                    }

                              );
                    item.branches = await sedes.ToListAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Se retorna la lista
            return result;
        }

        public async Task<QueryResult<ResponsUser>> GetUserByID(int id)
        {
            var result = new QueryResult<ResponsUser>();

            try
            {
                //Se obtienes datos de usuario
                var usersRespons = (from d in this._context.AuthUser
                                    join ur in this._context.userXrol on d.id equals ur.idUser
                                    where ur.estado == 1 && d.id == id
                                    select new ResponsUser
                                    {
                                        id = d.id,
                                        name = d.name,
                                        lastName = d.lastName,
                                        userName = d.userName,
                                        state = d.estado == 1 ? true : false,
                                        identificacion = "",
                                        email = d.mail,
                                        photo = ""
                                    }

                          );

                result.Items = await usersRespons.ToListAsync();

                //Se recorren los usuarios con el fin de buscar sus sedes y areas

                //Areas
                foreach (ResponsUser item in result.Items)
                {
                    //Se buscan las areas por usuario
                    var areas = (from usec in this._context.UsSection
                                 join sec in this._context.section on usec.idSection equals sec.id
                                 where usec.idUser == item.id && usec.state == 1
                                 select new SectionUser
                                 {
                                     access = true,
                                     area = new ResponsSection
                                     {
                                         id = sec.id,
                                         abbreviation = sec.abbreviation,
                                         name = sec.name,
                                         state = sec.estado == 1 ? true : false
                                     }
                                 }

                              );
                    item.areas = await areas.ToListAsync();
                    //Se buscan las sedes por usuario
                    //Consulta las sedes validas para ese usuario
                    var DemoSede = await this._objConf.GetConfig("DemograficoSede");

                    var sedes = (from It in this._context.DemoIt
                                 join dg in this._context.Demogra on It.idDemografico equals dg.id
                                 join Udi in this._context.uDIt on It.id equals Udi.idDemograficoItem
                                 where dg.id == Convert.ToInt16(DemoSede.Items.ElementAt(0).value) && dg.estado == 1 && Udi.estado == 1 && Udi.idUser == item.id
                                 select new BranchUser
                                 {
                                     access = true,
                                     branch = new ResponsBranch
                                     {
                                         id = Convert.ToInt16(It.id),
                                         code = It.codigo,
                                         abbreviation = "",
                                         name = It.demograficoItem.Trim(),
                                         state = It.estado == 1 ? true : false
                                     }
                                 }

                              );
                    item.branches = await sedes.ToListAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Se retorna la lista
            return result;
        }

        public async Task<QueryResult<ResponsUserByBraSec>> GetUserByBranSec(RequestBranchSection request)
        {
            var result = new QueryResult<ResponsUserByBraSec>();

            try
            {
                //demografico de sede
                var DemoSede = await this._objConf.GetConfig("DemograficoSede");
                //Se obtienen los usuarios por sede y areas
                var usersRespons = (from a in this._context.AuthUser
                                    join b in this._context.uDIt on a.id equals b.idUser
                                    join c in this._context.DemoIt on b.idDemograficoItem equals c.id
                                    join d in this._context.UsSection on a.id equals d.idUser
                                    join e in this._context.section on d.idSection equals e.id
                                    where c.idDemografico == Convert.ToInt16(DemoSede.Items.ElementAt(0).value) && c.id == request.idbranch && request.areas.Contains(e.id)
                                    select new ResponsUserByBraSec
                                    {
                                        id = a.id,
                                        name = a.name,
                                        lastName = a.lastName,
                                        userName = a.userName,
                                        state = a.estado == 1 ? true : false
                                    }
                          );

                result.Items = await usersRespons.Distinct().ToListAsync();
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
