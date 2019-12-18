using API_QM.Core.Models;
using System.Threading.Tasks;

namespace API_QM.Core
{
    public interface IConfiguracionRepository
    {

        Task<QueryResult<TrabajoSede>> GetJobBranch();
    }
}
