using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using API_ENTERPRISE.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _UssRepo;

        public UserService(IUserRepository UssRepo) {
            this._UssRepo = UssRepo;
        }
        public async Task<QueryResult<ResponsUser>> GetUser()
        {
            var obj = await this._UssRepo.GetUser();
            return obj;
        }

        public async Task<ResponsUser> GetUserByID(int id)
        {
            var obj = await this._UssRepo.GetUserByID(id);
            return obj.Items.ElementAt(0);
        }

        public async Task<QueryResult<ResponsUserByBraSec>> GetUserByBranSec(RequestBranchSection request)
        {
            var obj = await this._UssRepo.GetUserByBranSec(request);
            return obj;
        }
    }
}
