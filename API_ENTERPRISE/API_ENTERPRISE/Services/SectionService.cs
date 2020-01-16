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
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _section;
        public SectionService(ISectionRepository section)
        {
            this._section = section;
        }
        public async Task<QueryResult<ResponsSection>> GetSection()
        {
            var obj = await _section.GetSection();
            return obj;
        }

        public async Task<ResponsSection> GetSectionByID(int id)
        {
            var obj = await _section.GetSectionByID(id);
            return obj.Items.ElementAt(0);
        }
    }
}
