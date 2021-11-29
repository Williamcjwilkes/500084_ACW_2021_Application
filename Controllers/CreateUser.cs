using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Controllers
{
    public class CreateUser : Controller
    {
        public void CreateUserDB(string Username, string Password, string EmailAddress, string FirstName, string LastName, int AccType, string Subscriptions)
        {
            DBInterfacer newUser = new DBInterfacer();
            newUser.CreateUser(Username, Password, EmailAddress, FirstName, LastName, AccType, Subscriptions);

        }
    }
}
