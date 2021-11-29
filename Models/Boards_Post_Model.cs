using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Models
{
    public class Boards_Post_Model

    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Please enter a board to post to")]
        public int BoardFrom{ get; set; }

        [Required]
        [DisplayName("Please create a post title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Please create post content")]
        public string Message { get; set; }

        public DateTime Date { get; set; }
    }


}