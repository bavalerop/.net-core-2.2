using API_ENTERPRISE.Data;
using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly TodoContext _context;
        private readonly ConfiguracionRepository _objConf;

        public BranchRepository(TodoContext context)
        {
            this._context = context;
            this._objConf = new ConfiguracionRepository(this._context);
        }
        public async Task<QueryResult<ResponsBranch>> GetBranchesByID(int id)
        {
            var result = new QueryResult<ResponsBranch>();

            try
            {
                var TrabajoPorSede = await this._objConf.GetJobBranch();

                if (TrabajoPorSede.Items.ElementAt(0).value == "True")
                {
                    var DemoSede = await this._objConf.GetConfig("DemograficoSede");

                    var sed = (from It in this._context.DemoIt
                               where It.idDemografico == Convert.ToInt16(DemoSede.Items.ElementAt(0).value) && It.id == id
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
