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
    public class ConfiguracionService : IConfiguracionService
    {
        private readonly IConfiguracionRepository _configuration;

        public ConfiguracionService(IConfiguracionRepository configuration) {
            this._configuration = configuration;
        }

        public Task<QueryResult<ResponsConfig>> GetConfig(string key)
        {
           return  _configuration.GetConfig(key);
        }

        public Task<QueryResult<ResponsConfig>> GetJobBranch()
        {
            return _configuration.GetJobBranch();
        }
    }
}
