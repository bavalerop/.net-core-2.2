using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Repository.Interfaces;
using API_ENTERPRISE_SMC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Services
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
