using CMS.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CMS.Admin.Interfaces;

namespace CMS.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomers _customers;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICustomers customers)
        {
            _logger = logger;
            _customers = customers;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _customers.add();
            Console.WriteLine("Called from Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
