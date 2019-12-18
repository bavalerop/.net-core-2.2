using API_QM.Core;
using API_QM.Core.Models;
using API_QM.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace API_QM.Persistence
{
    public class ConfiguracionRepository : IConfiguracionRepository
    {
        private readonly TodoContext _context;

        public ConfiguracionRepository(TodoContext context) {
            this._context = context;
        }

        public async Task<QueryResult<TrabajoSede>> GetJobBranch()
        {
            var result = new QueryResult<TrabajoSede>();

            var Config = (from d in _context.Configuracion
                            select new TrabajoSede
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
