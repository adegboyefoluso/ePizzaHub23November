using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Implementation
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDBContext db) : base(db)
        {

        }
        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = _db.CartItems.Where(c => c.CartId == cartId && c.ItemId == itemId).FirstOrDefault();
            if (item != null)
            {
                _db.CartItems.Remove(item);
                return _db.SaveChanges();
            }
            return 0;
        }

        public Cart GetCart(Guid CartId)
        {
            var cart = _db.Carts.Include(c => c.CartItems).Where(c => c.IsActive == true && c.Id == CartId).FirstOrDefault();
            return cart;
        }

        public CartModel GetCartDetails(Guid CartId)
        {

            var cart = _db.Carts.Where(c => c.Id == CartId && c.IsActive).Select(c => new CartModel
            {
                Id = c.Id,
                UserId = c.UserId,
                CreatedDate = c.CreatedDate,
                Items = _db.CartItems
               .Where(ci => ci.CartId == CartId)
                   .Select(ci => new ItemModel
                   {
                       Id = ci.Item.Id,
                       Name = ci.Item.Name,
                       UnitPrice = ci.Item.UnitPrice,
                       Quantity = ci.Quantity,
                       Total = ci.Item.UnitPrice * ci.Quantity,
                       ImageUrl=ci.Item.ImageUrl
                   }).ToList()
            }).SingleOrDefault();

            return cart;





            //var mode = (from cart in _db.Carts
            //              where cart.Id == CartId && cart.IsActive == true
            //              select new CartModel
            //              {
            //                  Id = cart.Id,
            //                  UserId = cart.UserId,
            //                  CreatedDate = cart.CreatedDate,
            //                  Items = (from item in _db.CartItems
            //                           join product in _db.Items on item.Id equals product.Id
            //                           where item.CartId == CartId
            //                           select new ItemModel
            //                           {
            //                               Id = item.Id,
            //                               Name = product.Name,
            //                               UnitPrice = product.UnitPrice,
            //                               Quantity = item.Quantity,
            //                               Total = product.UnitPrice * item.Quantity
            //                           }).ToList()
            //              });



        }
        // assign userid to a cart if the user has not logged at  before 
        public int UpdateCart(Guid cartId, int userId)
        {
           Cart cart= _db.Carts.Where(c=>c.IsActive==true&& c.Id == cartId).FirstOrDefault();
            if (cart != null)
            {
                cart.UserId = userId;
               return _db.SaveChanges();
            }
            return 0;
        }
        //Login to update the item quntity  inside a cart 
        public int UpdateQuantity(Guid cartId, int id, int quantity)
        {
            bool flag = false;
            var cart= GetCart(cartId);
            if (cart != null)
            {
                var caritems= cart.CartItems.ToList();
                for (int i = 0; i < caritems.Count; i++) 
                {
                    if (caritems[i].ItemId == id)
                    {
                        flag = true;
                        caritems[i].Quantity += quantity;
                        break;
                    }
                }
                if (flag==true)
                {
                    cart.CartItems = caritems;
                    return _db.SaveChanges();
                }
            }
            return 0;
        }
    }
}
