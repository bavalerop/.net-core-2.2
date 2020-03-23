using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Repository.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<QueryResult<ResponsConfig>> GetJobBranch();
        Task<QueryResult<ResponsConfig>> GetConfig(string key);
    }
}
