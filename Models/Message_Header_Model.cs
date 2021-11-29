using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Models
{
    public class Message_Header_Model
    {

        public int ID { get; set; }

        [Required]
        [DisplayName("Please enter your username")]
        public string FromUser { get; set; }

        [Required]
        [DisplayName("Please enter the username of the user you wish to message")]
        public string ToUser { get; set; }
    }


}