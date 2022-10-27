using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using MySqlConnector;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void CreateUser()
        {

            Response.Redirect(HttpContext.Request.PathBase + "/login/CreateUser");
        }

        public void Login()
        {
            Response.Redirect(HttpContext.Request.PathBase+"/login");
        }

        public void Guest()
        {

            Response.Redirect(HttpContext.Request.PathBase + "/login/Guest");

        }
    }
}
