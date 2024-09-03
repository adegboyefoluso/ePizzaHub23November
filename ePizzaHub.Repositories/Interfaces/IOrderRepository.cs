using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        OrderModel GetOrderDetails(string orderId);
        IEnumerable<Order> GetUserOrders(int UserId);
    }
}
