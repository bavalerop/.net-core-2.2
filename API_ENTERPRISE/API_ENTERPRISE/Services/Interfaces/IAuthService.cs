using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponsAuth> ResponsToken(RequestAuthUser uss);

        Task<QueryResult<ResponsBranch>> GetBranches();

        string GenerateToken(AuthUser uss);
    }
}

