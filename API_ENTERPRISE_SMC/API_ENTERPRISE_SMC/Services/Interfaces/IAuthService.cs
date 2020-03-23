using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponsAuth> ResponsToken(RequestAuthUser uss);

        Task<QueryResult<ResponsBranch>> GetBranches();

        string GenerateToken(AuthUser uss);
    }
}

