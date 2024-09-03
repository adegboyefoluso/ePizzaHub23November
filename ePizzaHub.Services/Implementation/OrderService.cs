using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class OrderService : Service<Order>, IOrderService
    {
        IOrderRepository _orderRepo;
        public OrderService(IOrderRepository orderRepo) : base(orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public OrderModel GetOrderDetails(string OrderId)
        {
            return _orderRepo.GetOrderDetails(OrderId);
        }

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return _orderRepo.GetUserOrders(UserId);
        }

        public int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, AddressModel address)
        {
            Order order = new Order
            {
                PaymentId = paymentId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                Id = orderId,
                Street = address.Street,
                Locality = address.Locality,
                City = address.City,
                ZipCode = address.ZipCode,
                PhoneNumber = address.PhoneNumber
            };
            foreach (var item in cart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.Id,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total
                };
                order.OrderItems.Add(orderItem);
            }

            _orderRepo.Add(order);
            return _orderRepo.SaveChanges();
        }
    }

}
