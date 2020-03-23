using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Repository.Interfaces;
using API_ENTERPRISE_SMC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _order;

        public OrderService(IOrderRepository order)
        {
            this._order = order;
        }

        public Task<QueryResult<ResponsOrder>> CreateOrder(List<RequestOrder> ordenes)
        {
            return _order.CreateOrderAsync(ordenes);
        }
    }
}
