using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _500084_ACW_2021_Web_Application.Controllers
{
    public class Messaging_System : Controller
    {
        private List<Models.Message_Header_Model> msgHeaders { get; set; }
        private List<Models.Message_Model> messages { get; set; }

        public void NewMessage(string sentFrom, string sendTo)
        {
            DBInterfacer createNewMsg = new DBInterfacer();
            createNewMsg.CreateMessage(sentFrom, sendTo);
        }

        public void OpenMessageHeaders(string userName)
        {
            DBInterfacer getMessages = new DBInterfacer();
            List<Models.Message_Header_Model> msgHeaders = new List<Models.Message_Header_Model>();
            msgHeaders = getMessages.OpenUserMessageHeaders(userName);
        }

        public void OpenMessages(Models.Message_Header_Model header)
        {
            DBInterfacer getMessages = new DBInterfacer();
            messages = getMessages.OpenUserMessages(header);

        }

        public void SendMessages(int header, string content, string userName)
        {
            Models.Message_Model newMessage = new Models.Message_Model();

            newMessage.HeaderID = header;
            newMessage.Content = content;
            newMessage.Read = false;
            newMessage.IsFrom = userName;
            newMessage.Time = System.DateTime.Now;
        }
    }
}