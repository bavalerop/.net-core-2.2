using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services.Interfaces
{
    public interface IUserService
    {
        Task<QueryResult<ResponsUser>> GetUser();

        Task<ResponsUser> GetUserByID(int id);

        Task<QueryResult<ResponsUserByBraSec>> GetUserByBranSec(RequestBranchSection request);
    }
}
