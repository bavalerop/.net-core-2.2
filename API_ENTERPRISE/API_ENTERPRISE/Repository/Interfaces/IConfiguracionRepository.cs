using API_ENTERPRISE.Models;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<QueryResult<Config>> GetJobBranch();
        Task<QueryResult<Config>> GetConfig(string key);
    }
}
