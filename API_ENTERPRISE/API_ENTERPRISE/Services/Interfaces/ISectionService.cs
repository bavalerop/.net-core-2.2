using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services.Interfaces
{
    public interface ISectionService
    {
        Task<ResponsSection> GetSectionByID(int id);

        Task<QueryResult<ResponsSection>> GetSection();
    }
}
