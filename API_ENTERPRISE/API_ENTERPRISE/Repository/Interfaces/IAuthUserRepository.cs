using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository.Interfaces
{
    public interface IAuthUserRepository
    {
        Task<QueryResult<AuthUser>> GetUser(RequestAuthUser uss);
    }
}
