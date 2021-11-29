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
        public void CreateUserDB(Models.User_Model newUser)
        {
            DBInterfacer modifyDB = new DBInterfacer();
            modifyDB.CreateUser(newUser);

        }
    }
}
