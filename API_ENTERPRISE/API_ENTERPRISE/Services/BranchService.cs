using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using API_ENTERPRISE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branch;

        public BranchService(IBranchRepository branch) {
            this._branch = branch;
        }
        public async Task<ResponsBranch> GetBranchesByID(int id)
        {
            var obj = await _branch.GetBranchesByID(id);
            return obj.Items.ElementAt(0);
            
        }

        public async Task<QueryResult<ResponsBranch>> GetBranches()
        {
            var obj = await _branch.GetBranches();
            return obj;

        }
    }
}
