using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Models
{
    public class Boards_Model
    {

        public int ID { get; set; }

        [Required]
        [DisplayName("Please enter the board name")]
        public string Name { get; set; }

        public int NumUsers { get; set; }

        [Required]
        [DisplayName("Please verify if this board is being created for a society")]
        public bool IsSociety { get; set; }

        [Required]
        [DisplayName("Please enter a board description")]
        public string Description { get; set; }
    }


}