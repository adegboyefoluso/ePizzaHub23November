using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
   public interface ICartServices: IServices<Cart>
    {
        int GetCartCount(Guid id);
        CartModel GetCartDetails(Guid id);
        Cart AddItem(int UserId, Guid CartId,int itemid, decimal price, int quantity);
        int DeleteItem(Guid cartId, int ItemId);
        int UpdateQuantity(Guid cartId, int id, int quantity);
        int UpdateCart(Guid cartId, int userId);
    }
}
