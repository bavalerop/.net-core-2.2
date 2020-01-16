using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<QueryResult<ResponsUser>> GetUserByID(int id);

        Task<QueryResult<ResponsUser>> GetUser();

        Task<QueryResult<ResponsUserByBraSec>> GetUserByBranSec(RequestBranchSection request);
    }
}
