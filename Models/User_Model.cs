using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Models
{
    public class User_Model
    {
        [Required]
        [DisplayName("Please enter a username")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Please enter your first name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Please enter you last name ")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Please enter a password")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Please enter you email address")]
        public string EmailAddress { get; set; }
        [Required]
        [DisplayName("Please enter a valid option")]
        public int AccType { get; set; }  
        [Required]
        [DisplayName("Please tick at least one society to subscribe to ")]
        public string Subscriptions { get; set; }
    }


}
