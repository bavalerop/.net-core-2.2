using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<QueryResult<ResponsConfig>> GetJobBranch();
        Task<QueryResult<ResponsConfig>> GetConfig(string key);
    }
}
