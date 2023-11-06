﻿using ePizzaHub.Services.Implementation;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ePizzaHub.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       IItemService _itemService;

        public HomeController(ILogger<HomeController> logger,IItemService itemservice)
        {
            _logger = logger;
            _itemService = itemservice;
        }

        public IActionResult Index()
        {

            var items = _itemService.GetItems();
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}