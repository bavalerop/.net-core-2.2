using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Services.Interfaces
{
    public interface IConfiguracionService
    {

        Task<QueryResult<ResponsConfig>> GetConfig(string key);

        Task<QueryResult<ResponsConfig>> GetJobBranch();

    }
}
