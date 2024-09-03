using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface IOrderService : IServices<Order>
    {
        OrderModel GetOrderDetails(string OrderId);
        IEnumerable<Order> GetUserOrders(int UserId);

        int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, AddressModel address);
    }

}
