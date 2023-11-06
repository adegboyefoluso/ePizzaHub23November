using ePizzaHub.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ePizzaHub.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthService _authservice;

        public AccountController(IAuthService authService)
        {
            _authservice = authService;  
        }
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// non-Action method to create claim and Authentication  cookie
        /// </summary>
        /// <param name="user"></param>
        private async void GenerateTicket(UserModel user)
        {
            string strdata = JsonSerializer.Serialize(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, strdata),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,string.Join(",", user.Roles)),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };
            var identity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserModel user=_authservice.ValidateUser(model.Email, model.Password);
                if(user!=null)
                {
                    GenerateTicket(user);
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new {area="Admin"});
                    } 
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult UnAuthorize()
        {
            return View();
        }
    }
}
