using System.Linq;
using System.Threading.Tasks;
using API_ENTERPRISE.Data;
using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_ENTERPRISE.Repository
{
    public class ConfiguracionRepository : IConfiguracionRepository
    {
        private readonly TodoContext _context;

        public ConfiguracionRepository(TodoContext context)
        {
            this._context = context;
        }

        public async Task<QueryResult<ResponsConfig>> GetJobBranch()
        {
            var result = new QueryResult<ResponsConfig>();

            var Config = (from d in _context.Configuracion
                          where d.key == "TrabajoPorSede"
                          select new ResponsConfig
                          {
                              key = d.key,
                              value = d.value
                          }

                          );
            result.Items = await Config.ToListAsync();

            return result;
        }

        public async Task<QueryResult<ResponsConfig>> GetConfig(string key)
        {
            var result = new QueryResult<ResponsConfig>();

            var Config = (from d in _context.Configuracion
                          where d.key == key
                          select new ResponsConfig
                          {
                              key = d.key,
                              value = d.value
                          }

                          );
            result.Items = await Config.ToListAsync();

            return result;
        }
    }
}
