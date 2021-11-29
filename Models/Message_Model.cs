using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Models
{
    public class Message_Model
    {

        public int ID { get; set; }

        public int HeaderID { get; set; }

        [Required]
        [DisplayName("Please enter your message ")]
        public string Content { get; set; }

        public bool Read { get; set; }

        public string IsFrom { get; set; }

        public DateTime Time { get; set; }
    }


}