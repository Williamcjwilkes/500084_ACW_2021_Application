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

        public string dBase = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Systems_Analysis_Users.db");
        public String[] GetUserData(string username) //Gets the userdata from the database
        {
            String[] userData = new String[8];
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            string query = "SELECT * FROM 'Users' where username='" + username + "'";
            var command = new SQLiteCommand(query, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            try
            {
                userData[0] = dataReader.GetString(0);
                userData[1] = dataReader.GetString(1);
                userData[2] = dataReader.GetString(2);
                userData[3] = dataReader.GetString(3);
                userData[4] = dataReader.GetString(4);
                userData[5] = dataReader.GetString(5);
                userData[6] = dataReader.GetString(6);
                userData[7] = dataReader.GetString(7);
            }
            catch (Exception)
            {
                Console.WriteLine("If something errored out here, it *probably* means that nothing is in the database. Return null to notify rest of program of issue");
                userData[0] = null;
                return userData;
            }
            connection.Close();
            return userData;
        }

        public void CreateUser(string username, string password, string email, string forename, string surname, int accType, string subscriptions) //Adds a user to the database, can be any user type
        {
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            string query = "INSERT INTO Users(username, password, email, forename, surname, accType, subscriptions) VALUES ('" + username + "','" + password + "','" + email + "','" + forename + "','" + surname + "','" + accType + "','" + subscriptions + "')";
            var command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();

            //add confirmation
            connection.Close();
        }

        public void SendMessage(string sender, string recipient, string message)
        {
            string msgHeader;
            string request = "SELECT id FROM 'Message_Header' WHERE fromID = '" + sender + "' AND toID = '" + recipient + "' OR fromID = '" + recipient + "' AND toID = '" + sender + "'";

            var connection = new SQLiteConnection(dBase);
            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();
            msgHeader = dataReader.ToString();

            DateTime time = DateTime.Now;
            request = "INSERT INTO Messages(headerID, content, read, isFrom, time) VALUES ('" + msgHeader + "','" + message + "','" + sender + "','" + time + "')";
            connection = new SQLiteConnection(dBase);
            command = new SQLiteCommand(request, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<String[]> OpenMessage(string sender, string recipient)
        {
            string msgHeader;           
            string request = "SELECT id FROM 'Message_Header' WHERE fromID = '" + sender + "' AND toID = '" + recipient + "' OR fromID = '" + recipient + "' AND toID = '" + sender + "'";
            List<String[]> messages = new List<String[]>();

            var connection = new SQLiteConnection(dBase);
            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();
            msgHeader = dataReader.ToString();

            if(msgHeader == "")
            {
                request = "INSERT INTO Message_Header(fromID, toID) VALUES ('" + sender + "','" + recipient + "')";
            }

            request = "SELECT content, read, isFrom, time FROM 'Messages' WHERE headerID = '" + msgHeader;
            command = new SQLiteCommand(request, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string[] result = new string[4];
                result[0] = dataReader.GetString(0);
                result[1] = dataReader.GetString(1);
                result[2] = dataReader.GetString(2);
                result[3] = dataReader.GetString(3);
                messages.Add(result);
            }

            connection.Close();
            return messages;
        }

        public List<String[]> GetAllBoards()
        {
            string request = "SELECT name, numUsers, isSociety, description FROM 'Boards'";
            List<String[]> boards = new List<String[]>();
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string[] result = new string[4];
                result[0] = dataReader.GetString(0);
                result[1] = dataReader.GetString(1);
                result[2] = dataReader.GetString(2);
                result[3] = dataReader.GetString(3);
                boards.Add(result);
            }

            connection.Close();
            return boards;
        }

        public List<String[]> GetUserBoards(string subscriptions)
        {
            List<String[]> boards = new List<String[]>();
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

                string[] result = new string[4];
                result[0] = dataReader.GetString(0);
                result[1] = dataReader.GetString(1);
                result[2] = dataReader.GetString(2);
                result[3] = dataReader.GetString(3);
                boards.Add(result);
            }

            connection.Close();
            return boards;
        }

        public List<String[]> GetBoardMessages(string id)
        {
            string request = "SELECT title, message, date FROM 'Board_Messages' WHERE boardFrom = '" + id + "'";
            List<String[]> boardMessages = new List<String[]>();
            var connection = new SQLiteConnection(dBase);

            connection.Open();
            var command = new SQLiteCommand(request, connection);
            SQLiteDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string[] result = new string[3];
                result[0] = dataReader.GetString(0);
                result[1] = dataReader.GetString(1);
                result[2] = dataReader.GetString(2);
                boardMessages.Add(result);
            }

            connection.Close();
            return boardMessages;
        }



        // created for testing
        // please remove from 
    }
}