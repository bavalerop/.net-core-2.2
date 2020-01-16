using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository.Interfaces
{
    public interface ISectionRepository
    {

        Task<QueryResult<ResponsSection>> GetSectionByID(int id);

        Task<QueryResult<ResponsSection>> GetSection();
    }
}
