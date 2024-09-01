using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Implementation;
using ePizzaHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class CartController : BaseController
    {
        ICartServices _cartServices;
        Guid Cartid
        {
            // First check if  Cartid is inside cookie, if is not there,  then  create a new Guid and add it to  the cookies 
            get
            {
                Guid Id;
                if (Request.Cookies["CId"] == null)
                {
                    Id= Guid.NewGuid();
                    Response.Cookies.Append("CId",Id.ToString(), new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(5)
                    });
                }
                else
                {
                    Id = Guid.Parse(Request.Cookies["CId"]) ;
                }
                return Id;
            }
        }
        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices; 
        }
        public IActionResult Index()
        {

            CartModel cartModel = _cartServices.GetCartDetails(Cartid);// use the cartId created in the UI  to get the cart details 
            return View(cartModel);
        }
        [Route("Cart/AddToCart/{ItemId}/{UnitPrice}/{Quantity}")]
        public IActionResult AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            int UserId = CurrentUser != null ? CurrentUser.Id : 0;
            if (ItemId > 0 && UnitPrice > 0 && Quantity > 0)
            {
                Cart cart = _cartServices.AddItem(UserId, Cartid, ItemId, UnitPrice, Quantity);
                if (cart != null)
                {
                    return Json(new { status = "success", count = cart.CartItems.Count });
                }
            }
            return Json(new { status = "failed", count = 0 });
        }


        [Route("Cart/DeleteItem/{ItemId}")]
        public IActionResult DeleteItem(int ItemId)
        {
            int count = _cartServices.DeleteItem(Cartid, ItemId);
            return Json(count);
        }

        [Route("Cart/UpdateQuantity/{Id}/{Quantity}")]
        public IActionResult UpdateQuantity(int Id, int Quantity)
        {
            int count = _cartServices.UpdateQuantity(Cartid, Id, Quantity);
            return Json(count);
        }

        public IActionResult GetCartCount()
        {
            int count = _cartServices.GetCartCount(Cartid);
            return Json(count);
        }

    }
}
