using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<QueryResult<ResponsOrder>> CreateOrderAsync(List<RequestOrder> ordenes);
    }
}
