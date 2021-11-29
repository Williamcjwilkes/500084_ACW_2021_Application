using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _500084_ACW_2021_Web_Application.Models;

namespace _500084_ACW_2021_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController>_logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // need to compare the input to the database user and password
        public IActionResult Login(string Username, string Password)
        {
            DBInterfacer userData = new DBInterfacer();
            userData.GetUserData(Username);
            User_Model user = new User_Model(); 

            if ( user.Password == Password)
            {
                return View("success", user);
            }
            else
            {
                return View("Failure", user);
            }
        }


        // returning views
        public IActionResult Homepage()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult More()
        {
            return View();
        }
        public IActionResult Boardoftheday()
        {
            return View();
        }
        public IActionResult Help()
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
