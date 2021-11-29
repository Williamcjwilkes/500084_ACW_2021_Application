using _500084_ACW_2021_Web_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _500084_ACW_2021_Web_Application.Controllers
{
    public class DisplayBoards : Controller
    {
        [HttpGet]
        public ContentResult Index(User_Model user)
        {

            // for each board in user subscription write out to page
            return base.Content(@"
               <div class='col-lg-3 col-sm-1 mb-4'>
                 <div class='card h-100'>
                    <a href='#'>< img class='card-img-top card-post-styles' src='https://cdn2.hubspot.net/hubfs/1484553/webchatwebopt-700x400.jpg' alt=''></a>
                    <div class='card-body'>
                        <h4 class='card-title'>
                            <a href = '#' > Computer Science - Hackathon</a>
                        </h4>
                        <p class='card-text'>This week is the Hackathon, can you build a game over a week ?? </p>
                    </div>
                </div>");
        }
    }
}
