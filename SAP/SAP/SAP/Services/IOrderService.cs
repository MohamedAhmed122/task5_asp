using SAP.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Services
{
    public interface IOrderService
    {
        Task<OrderListViewModel> GetUserOrdersAsync();

        Task CreateOrderFromCartAsync();

        Task<OrderViewModel> GetOrderAsync(Guid id);
    }
}
