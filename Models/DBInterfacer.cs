using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace _500084_ACW_2021_Web_Application
{
    public class DBInterfacer
    {

        private string dBase = @"URI=file:" + Directory.GetCurrentDirectory() + "\\Models\\Web_Application.db";

        public Models.User_Model GetUserData(string username) //Gets the userdata from the database and inputs it into a user object
        {
            Models.User_Model currentUser = new Models.User_Model();
            string query = "SELECT * FROM 'Users' where username='" + username + "'";
            var connection = new SQLiteConnection(dBase);
            connection.Open();
  
            var command = new SQLiteCommand(query, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    currentUser.Username = dataReader.GetString(1);
                    currentUser.Password = dataReader.GetString(2);
                    currentUser.EmailAddress = dataReader.GetString(3);
                    currentUser.FirstName = dataReader.GetString(4);
                    currentUser.LastName = dataReader.GetString(5);
                    currentUser.AccType = dataReader.GetString(6);
                    currentUser.Subscriptions = dataReader.GetString(7);
                }  
            
            connection.Close();
            return currentUser;
        }

        public void CreateUser(Models.User_Model newUser) //Adds a user to the database, can be any user type
        {
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            string query = "INSERT INTO Users(username, password, email, forename, surname, accType, subscriptions) VALUES ('" + newUser.Username + "','" + newUser.Password + "','" + newUser.EmailAddress + "','" + newUser.FirstName + "','" + newUser.LastName + "','" + newUser.AccType + "','" + newUser.Subscriptions + "')";
            var command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();

            //add confirmation
            connection.Close();
        }

        public void CreateMessage(string fromUser, string toUser) //Creates a new message header entry for when a user wants to message another user they have not messaged before
        {
            string request = "INSERT INTO Message_Header(fromUser, toUser) VALUES ('" + fromUser + "','" + toUser + "')";

            var connection = new SQLiteConnection(dBase);
            connection.Open();
            var command = new SQLiteCommand(request, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void SendMessage(Models.Message_Model newMsg) //Adds a message object that has been passed to this method into the database
        {
            string request = "INSERT INTO Messages(headerID, content, read, isFrom, time) VALUES ('" + newMsg.HeaderID + "','" + newMsg.Content + "','" + newMsg.Read + "','" + newMsg.IsFrom + "','" + newMsg.Time + "')";
            var connection = new SQLiteConnection(dBase);
            connection.Open();
            var command = new SQLiteCommand(request, connection);

            connection = new SQLiteConnection(dBase);
            command = new SQLiteCommand(request, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Models.Message_Header_Model> OpenUserMessageHeaders(string userName) //Opens all the message headers associated to the logged in user, allowing then for a search of specific message logs
        {
            List<Models.Message_Header_Model> msgHeaders = new List<Models.Message_Header_Model>();
            string request = "SELECT id, fromUser, toUser FROM 'Message_Header' WHERE fromID = '" + userName + "' OR toID = '" + userName + "'";

            var connection = new SQLiteConnection(dBase);
            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Models.Message_Header_Model currentHeader = new Models.Message_Header_Model();

                currentHeader.ID = Int32.Parse(dataReader.GetString(0));
                currentHeader.FromUser = dataReader.GetString(1);
                currentHeader.ToUser = dataReader.GetString(2);
                msgHeaders.Add(currentHeader);
            }

            connection.Close();
            return msgHeaders;
        }

        public List<Models.Message_Model> OpenUserMessages(Models.Message_Header_Model msgHeader) //Opens a message log with another user based on a message header pointer
        {
            List<Models.Message_Model> messages = new List<Models.Message_Model>();
            string request = "SELECT id, headerID, content, read, isFrom, time FROM 'Messages' WHERE headerID = '" + msgHeader.ID + "'";

            var connection = new SQLiteConnection(dBase);
            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            command = new SQLiteCommand(request, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Models.Message_Model message = new Models.Message_Model();

                message.ID = Int32.Parse(dataReader.GetString(0));
                message.HeaderID = Int32.Parse(dataReader.GetString(1));
                message.Content = dataReader.GetString(2);
                message.Read = true;
                message.IsFrom = dataReader.GetString(4);
                message.Time = DateTime.Parse(dataReader.GetString(5));
                messages.Add(message);
            }

            connection.Close();
            return messages;
        }

        public void CreateBoard(Models.Boards_Model newBoard) //Creates a new board
        {
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            string query = "INSERT INTO Boards(name, isSociety, description) VALUES ('" + newBoard.Name + "','" + newBoard.IsSociety + "','" + newBoard.Description + "')";
            var command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();

            //add confirmation
            connection.Close();
        }

        public void SubscribetoBoard(string boardName, Models.User_Model currentUser) //Subscribes the user to a selected board
        {
            var connection = new SQLiteConnection(dBase);
            int numUsers = 0;         

            connection.Open();
            string query = "SELECT numUsers FROM 'Boards' WHERE name = '" + boardName + "'";
            var command = new SQLiteCommand(query, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            numUsers = Int32.Parse(dataReader.GetString(0));

            query = "UPDATE Boards SET numUsers = '" + (numUsers+1) + "' WHERE name = '" + boardName + "'";
            command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();

            currentUser.Subscriptions = currentUser.Subscriptions + boardName + ",";
            query = "UPDATE Users SET subsciptions = '" + currentUser.Subscriptions + "' WHERE username = '" + currentUser.Username + "'";

            //add confirmation
            connection.Close();
        }

        public void UnsubBoard(string boardName, Models.User_Model currentUser) //Unsubscribes the user to a selected board
        {
            var connection = new SQLiteConnection(dBase);
            int numUsers = 0;
            string newsubs = "";
            int c = 0;

            connection.Open();
            string query = "SELECT numUsers FROM 'Boards' WHERE name = '" + boardName + "'";
            var command = new SQLiteCommand(query, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            numUsers = Int32.Parse(dataReader.GetString(0));

            query = "UPDATE Boards SET numUsers = '" + (numUsers - 1) + "' WHERE name = '" + boardName + "'";
            command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();

            string[] userBoards = (currentUser.Subscriptions.Split(','));

            while(userBoards.Length < c)
            {
                if(userBoards[c] == boardName)
                {
                    c++;
                }
                else
                {
                    newsubs = newsubs + userBoards[c] + ",";
                }
            }

            query = "UPDATE Users SET subsciptions = '" + newsubs + "' WHERE username = '" + currentUser.Username + "'";

            //add confirmation
            connection.Close();
        }

        public List<Models.Boards_Model> GetAllBoards() //Gets all the boards that have been created, could maybe look into adding a page system later?
        {
            string request = "SELECT name, numUsers, isSociety, description FROM 'Boards'";
            List<Models.Boards_Model> boards = new List<Models.Boards_Model> ();
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Models.Boards_Model board = new Models.Boards_Model();

                board.ID = Int32.Parse(dataReader.GetString(0));
                board.Name = dataReader.GetString(1);
                board.NumUsers = Int32.Parse(dataReader.GetString(2));
                board.IsSociety = bool.Parse(dataReader.GetString(3));
                board.Description = dataReader.GetString(4);
                boards.Add(board);
            }

            connection.Close();
            return boards;
        }

        public List<Models.Boards_Model> GetUserBoards(string subscriptions) //Gets the boards that the user is subscribed to, requires a method sending the logged in users subscriptions
        {
            List<Models.Boards_Model> boards = new List<Models.Boards_Model>();
            string[] userBoards = (subscriptions.Split(','));
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            SQLiteDataReader dataReader;

            for(int c = 0; c < userBoards.Length; c++)
            {
                string request = "SELECT name, numUsers, isSociety, description FROM 'Boards' WHERE id = '" + userBoards[c] + "'";
                var command = new SQLiteCommand(request, connection);
                dataReader = command.ExecuteReader();
                dataReader.Read();

                Models.Boards_Model board = new Models.Boards_Model();

                board.ID = Int32.Parse(dataReader.GetString(0));
                board.Name = dataReader.GetString(1);
                board.NumUsers = Int32.Parse(dataReader.GetString(2));
                board.IsSociety = bool.Parse(dataReader.GetString(3));
                board.Description = dataReader.GetString(4);
                boards.Add(board);
            }

            connection.Close();
            return boards;
        }

        public List<Models.Boards_Post_Model> GetBoardPosts(int fromID) //Gets all the posts associated with a board to be displayed
        {
            string request = "SELECT title, message, date FROM 'Board_Messages' WHERE boardFrom = '" + fromID + "'";
            List<Models.Boards_Post_Model> boardPosts = new List<Models.Boards_Post_Model>();
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Models.Boards_Post_Model post = new Models.Boards_Post_Model();

                post.ID = Int32.Parse(dataReader.GetString(0));
                post.BoardFrom = Int32.Parse(dataReader.GetString(1));
                post.Title = dataReader.GetString(2);
                post.Message = dataReader.GetString(3);
                post.Date = DateTime.Parse(dataReader.GetString(4));
                boardPosts.Add(post);
            }

            connection.Close();
            return boardPosts;
        }

    }
}