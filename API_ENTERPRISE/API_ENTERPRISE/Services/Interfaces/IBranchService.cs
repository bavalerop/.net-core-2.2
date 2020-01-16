using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services.Interfaces
{
    public interface IBranchService
    {
        Task<ResponsBranch> GetBranchesByID(int id);

        Task<QueryResult<ResponsBranch>> GetBranches();
    }
}
