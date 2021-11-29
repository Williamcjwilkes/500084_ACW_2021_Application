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
        private List<Models.Message_Header_Model> msgHeaders { get; set; } //Stores the users message headers once they have been grabbed from the db so that they don't have to be fetched again when view has been changed
        private List<Models.Message_Model> messages { get; set; } //Stores the users messages once they have been grabbed from the db so that they don't have to be fetched again when view has been changed

        public void NewMessage(string sentFrom, string sendTo) //Sends the info to create a new message header to the database, requires the usernames of the logged in user and the user the message is being sent to
        {
            DBInterfacer createNewMsg = new DBInterfacer();
            createNewMsg.CreateMessage(sentFrom, sendTo);
        }

        public void OpenMessageHeaders(string userName) //Requests the database interfacer to fetch message headers associated with this user then stores them in a variable for view to use. Requires username to be passed from view
        {
            DBInterfacer getMessages = new DBInterfacer();
            List<Models.Message_Header_Model> msgHeaders = new List<Models.Message_Header_Model>();
            msgHeaders = getMessages.OpenUserMessageHeaders(userName);
        }

        public void OpenMessages(Models.Message_Header_Model header) //Requests the database interfacer to get the messages associated to the given header and stores them in a variable. Requires header to be passed from view
        {
            DBInterfacer getMessages = new DBInterfacer();
            messages = getMessages.OpenUserMessages(header);

        }

        public void SendMessages(int header, string content, string userName) //Sends a new message to the database interfacer to be stored. Requires the message header, message body and username of the user to be passed from the view
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