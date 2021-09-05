using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace PasswordKeeper
{
    public class DBManager
    {
        public SQLiteConnection Connection;
        private static string dbName = "database";
        private static string dbFilePath = "./data/" + dbName.ToString() + ".pkd";
        private string fileName = "";
        public void CreateDatabase()
        {
           
            try
            {

                if (!Directory.Exists("./data/"))
                {
                    Directory.CreateDirectory("./data/");
                }
                if (!File.Exists(dbFilePath))
                {
                    SQLiteConnection.CreateFile(dbFilePath);

                }
                fileName = dbFilePath;
                Connection = new SQLiteConnection(string.Format(
                      //  "Data Source={0};Version=3;New=true;Compress=True;Password=nvnnjk" + dbName + "lo9us", dbFilePath));
                //"Data Source={0};Version=3;New=true;Compress=True;Password=keepmypassto4ever", dbFilePath));
                "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
                Connection.Open();
                string cmd;
                cmd = " CREATE TABLE IF NOT EXISTS [KEEPMYPASS] ( "
                + "   [Id] INTEGER PRIMARY KEY AUTOINCREMENT "
                + " , [Alias] TEXT "
                + " , [Username] TEXT "
                + " , [Password] TEXT "
                + ")";
                //Connection.ExecuteNon(cmd, null);

                SQLiteCommand sQLiteCommand = new SQLiteCommand(cmd, Connection);
                sQLiteCommand.ExecuteNonQuery();
                Connection.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public string ReadData(int id)
        {

            Connection = new SQLiteConnection(string.Format(
            //  "Data Source={0};Version=3;New=true;Compress=True;Password=nvnnjk" + dbName + "lo9us", dbFilePath));
            //"Data Source={0};Version=3;New=true;Compress=True;Password=keepmypassto4ever", dbFilePath));
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();
            string cmd = "SELECT * FROM KEEPMYPASS WHERE ID = " + id;
            SQLiteCommand sQLiteCommand = new SQLiteCommand(cmd, Connection);
            try
            {
                SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
                if (sQLiteDataReader.Read())
                {
                    string data = sQLiteDataReader["Alias"].ToString()+ "-" + sQLiteDataReader["Username"].ToString() + "-"+ sQLiteDataReader["Password"].ToString();
                    return data;
                }
                else return "";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }
        public void InsertTestData()
        {
            Connection = new SQLiteConnection(string.Format(
            //  "Data Source={0};Version=3;New=true;Compress=True;Password=nvnnjk" + dbName + "lo9us", dbFilePath));
            //"Data Source={0};Version=3;New=true;Compress=True;Password=keepmypassto4ever", dbFilePath));
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();

            string firstData = "INSERT INTO KEEPMYPASS (Alias, Username, Password) values ('Test', 'TestUser', 'TestPass')";
            SQLiteCommand sQLiteCommand = new SQLiteCommand(firstData, Connection);
            sQLiteCommand.ExecuteNonQuery();
            Connection.Close();
        }
        public void InsertData(string alias, string username, string password)
        {
            Connection = new SQLiteConnection(string.Format(
            //  "Data Source={0};Version=3;New=true;Compress=True;Password=nvnnjk" + dbName + "lo9us", dbFilePath));
            //"Data Source={0};Version=3;New=true;Compress=True;Password=keepmypassto4ever", dbFilePath));
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();
            string cmd = "INSERT INTO KEEPMYPASS (Alias, Username, Password) values ('" + alias.ToString() + "', " + "'"+username.ToString() +"', '" + password.ToString()+"')";
            SQLiteCommand sQLiteCommand = new SQLiteCommand(cmd, Connection);
            sQLiteCommand.ExecuteNonQuery();
            Connection.Close();

        }
        public void UpdateData(string id, string alias, string username, string password)
        {
            Connection = new SQLiteConnection(string.Format(
            //  "Data Source={0};Version=3;New=true;Compress=True;Password=nvnnjk" + dbName + "lo9us", dbFilePath));
            //"Data Source={0};Version=3;New=true;Compress=True;Password=keepmypassto4ever", dbFilePath));
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();
            string cmd = "UPDATE KEEPMYPASS SET ALIAS = '"+alias+"', USERNAME = '"+username +"', PASSWORD= '"+password+"' WHERE ID='"+id+"'";
            SQLiteCommand sQLiteCommand = new SQLiteCommand(cmd, Connection);
            sQLiteCommand.ExecuteNonQuery();
            Connection.Close();

        }
        public void DeleteEntry(string Id)
        {
                Connection = new SQLiteConnection(string.Format(
            //  "Data Source={0};Version=3;New=true;Compress=True;Password=nvnnjk" + dbName + "lo9us", dbFilePath));
            //"Data Source={0};Version=3;New=true;Compress=True;Password=keepmypassto4ever", dbFilePath));
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();
            string cmd = "DELETE FROM KEEPMYPASS WHERE ID = '" + Id + "'";
            SQLiteCommand sQLiteCommand = new SQLiteCommand(cmd, Connection);
            sQLiteCommand.ExecuteNonQuery();
            Connection.Close();
        }

    }
   
}
