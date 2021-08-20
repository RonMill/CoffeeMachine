using System;
using System.Data.SQLite;
using System.IO;
using System.Text;
using SharedObjects;

namespace DatabaseService
{
    public class DatabaseManager : IDisposable
    {
        private readonly SQLiteConnection sQLiteConnection;
        private readonly SQLiteCommand sQLiteCommand;
        public DatabaseManager()
        {
            sQLiteConnection = new SQLiteConnection("Data Source=CoffeeDatabase.db;Version=3");
            sQLiteCommand = new SQLiteCommand();
            sQLiteCommand.Connection = sQLiteConnection;
        }
        public void CreateDatabase()
        {
            if (!File.Exists("CoffeeDatabase.db"))
                SQLiteConnection.CreateFile("CoffeeDatabase.db");
        }
        public void CreateDatabaseTable()
        {
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS User (ID INTEGER PRIMARY KEY AUTOINCREMENT, FirstName varchar(255) NOT NULL, LastName varchar(255), " +
                "Email varchar(255), Street varchar(255), Housenumber varchar(255), Postcode varchar(255), City varchar(255), Username varchar(255), Password varchar(255))";
            sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
        }

        public bool Login(IUser user)
        {
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = $"SELECT Username, Password FROM User WHERE Username='{user.Username}' AND Password='{user.Password}'";
            string str = sQLiteCommand.ExecuteScalar()?.ToString();
            sQLiteConnection.Close();
            return !string.IsNullOrEmpty(str);
        }

        public int AddUser(IUser user)
        {
            CreateDatabase();
            CreateDatabaseTable();
            sQLiteConnection.Open();
            //sQLiteCommand.CommandText = "INSERT INTO User VALUES (" + user.FirstName + "," + user.LastName + "," + user.Email + "," + user.Street
            //+ "," + user.Housenumber + "," + user.Postcode + "," + user.City + "," + user.Username + "," + user.Password + ")";
            //sQLiteCommand.CommandText = string.Join("\',\'", "INSERT INTO User VALUES (" + null +",\'"+ user.FirstName, user.LastName, user.Email, user.Street, user.Housenumber, user.Postcode, user.City, user.Username, user.Password+"\')");

            sQLiteCommand.CommandText = $"INSERT INTO User VALUES ( null, '{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Street}', '{ user.Housenumber}', '{user.Postcode}', '{ user.City}', '{ user.Username}', '{ user.Password }' )";

            int a = sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
            return a;
        }

        public void Dispose()
        {
            sQLiteCommand.Dispose();
            sQLiteConnection.Dispose();
        }
    }
}
