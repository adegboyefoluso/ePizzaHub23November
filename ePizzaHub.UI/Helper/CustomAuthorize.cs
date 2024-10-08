﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ePizzaHub.UI.Helper
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }   
        public void OnAuthorization(AuthorizationFilterContext context)
        
        {
            //check for authentication
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    context.Result = new RedirectToActionResult("UnAuthorize", "Account", new { area = "" });
                }


            }
            else
            {
                string returnurl = context.HttpContext.Request.Path;
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnurl=returnurl });
            }
        }
    }
}
