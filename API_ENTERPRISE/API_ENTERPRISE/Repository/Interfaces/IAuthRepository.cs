using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<QueryResult<AuthUser>> GetAccess(RequestAuthUser uss);

        Task<QueryResult<ResponsBranch>> GetBranches();
    }
}
