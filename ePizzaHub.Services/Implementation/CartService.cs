using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class CartService : Service<Cart>, ICartServices
    {
        ICartRepository _cartrepo;
        IRepository<CartItem> _cartItemRepo;
        IConfiguration _config;
        public CartService(ICartRepository cardrepo, IRepository<CartItem> cartItemrepo, IConfiguration config):base(cardrepo)
        {
             _cartrepo = cardrepo;
            _cartItemRepo = cartItemrepo;
            _config = config;
        }
        public Cart AddItem(int UserId, Guid CartId, int itemid, decimal price, int quantity)
        {
            
            try
            {
                Cart cart = _cartrepo.GetCart(CartId);
                // First check if item already exist in the cart
                if (cart == null)
                {
                    // if there is no cart, this create  a new  cart
                    cart = new Cart { Id = CartId, UserId = UserId, CreatedDate = DateTime.Now, IsActive = true };
                    // create a cartitem  and add it to the cart
                    CartItem item = new CartItem { ItemId = itemid, Quantity = quantity, UnitPrice = price, CartId = CartId };
                    cart.CartItems.Add(item);

                    _cartrepo.Add(cart);
                    _cartrepo.SaveChanges();
                }
                else
                {
                    CartItem item = cart.CartItems.Where(p => p.ItemId == itemid).FirstOrDefault();
                    if (item != null)
                    {
                        item.Quantity += quantity;
                        _cartItemRepo.Update(item);
                        _cartItemRepo.SaveChanges();
                    }
                    else
                    {
                        item = new CartItem { ItemId = itemid, Quantity = quantity, UnitPrice = price, CartId = CartId };
                        cart.CartItems.Add(item);
                        _cartItemRepo.SaveChanges();
                    }
                }

                return cart;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public int DeleteItem(Guid cartId, int ItemId)
        {
           return _cartrepo.DeleteItem(cartId, ItemId);
        }

        public int GetCartCount(Guid id)
        {
           var cart=_cartrepo.GetCart(id);
            if(cart != null)
            {
                return cart.CartItems.Count();
            }
            return 0;
        }

        public CartModel GetCartDetails(Guid id)
        {
            var model= _cartrepo.GetCartDetails(id);
            if (model != null && model.Items.Count > 0)
            {
                decimal subtotal =0;
                foreach (var item in model.Items)
                {
                    item.Total = item.UnitPrice * item.Quantity;
                    subtotal += item.Total;
                }
                model.Total = subtotal;
                model.Tax = Math.Round((model.Total * Convert.ToInt32(_config["Tax:VAT"])) / 100, 2);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            return _cartrepo.UpdateCart(cartId, userId); ;
        }

        public int UpdateQuantity(Guid cartId, int id, int quantity)
        {
            return _cartrepo.UpdateQuantity(cartId, id, quantity); 
        }
    }
}
