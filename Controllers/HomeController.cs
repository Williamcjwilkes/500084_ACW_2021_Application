 
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
        public IActionResult Login(string userName, string password)
        {
            DBInterfacer userData = new DBInterfacer();
            // create the empty user model 
            User_Model user = new User_Model();
            user = userData.GetUserData(userName);
             
            if ( password == user.Password)
            {
                return View("Homepage", user);
            }
            else 
            {
                return View("Index", user);
            }
        }


        // returning views
        public IActionResult Homepage(User_Model user)
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser(User_Model createUser)
        {
            DBInterfacer user = new DBInterfacer();
            user.CreateUser(createUser);
           
            return View("Index");
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

        public IActionResult createBoard(Boards_Model board)
        {
            DBInterfacer boards = new DBInterfacer();
            boards.CreateBoard(board);
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
