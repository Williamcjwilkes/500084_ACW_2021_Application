using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Models
{
    public class User_Model
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public int AccType { get; set; }
        public string Subscriptions { get; set; }


        public void CreateUserDB(string Username, string Password, string EmailAddress, string FirstName, string LastName, int AccType, string Subscriptions )
        {
            DBInterfacer newUser = new DBInterfacer();

            newUser.CreateUser(Username, Password, EmailAddress, FirstName, LastName, AccType, Subscriptions);

        }
    }
}
