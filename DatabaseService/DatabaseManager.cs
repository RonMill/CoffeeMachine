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
            sQLiteCommand = new SQLiteCommand
            {
                Connection = sQLiteConnection
            };
        }
        public void CreateDatabaseTable()
        {
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS User (ID INTEGER PRIMARY KEY AUTOINCREMENT, FirstName varchar(255) NOT NULL, LastName varchar(255), " +
                "Email varchar(255), Street varchar(255), Housenumber varchar(255), Postcode varchar(255), City varchar(255), Username varchar(255), Password varchar(255), Balance varchar(255))";
            sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
        }

        public bool Login(IUser user)
        {
            CreateDatabaseTable();
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = $"SELECT Username, Password FROM User WHERE Username='{user.Username}' AND Password='{user.Password}'";
            string str = sQLiteCommand.ExecuteScalar()?.ToString();
            sQLiteConnection.Close();
            return !string.IsNullOrEmpty(str);
        }

        public int AddUser(IUser user)
        {
            CreateDatabaseTable();
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = $"INSERT INTO User VALUES ( null, '{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Street}', '{ user.Housenumber}', '{user.Postcode}', '{ user.City}', '{ user.Username}', '{ user.Password }', '{ user.Balance }')";
            int a = sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
            return a;
        }
        public void GetUser(IUser user)
        {
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = $"SELECT FirstName, LastName, Email, Balance FROM User WHERE Username='{user.Username}'";
            /*The reason for the using statement is to ensure that the object is disposed as soon as it goes out of scope, 
             * and it doesn't require explicit code to ensure that this happens.*/
            using (SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader())
            {
                sQLiteDataReader.Read();
                user.FirstName = sQLiteDataReader.GetString(0);
                user.LastName = sQLiteDataReader.GetString(1);
                user.Email = sQLiteDataReader.GetString(2);
                user.Balance = sQLiteDataReader.GetString(3);
            }
            sQLiteConnection.Close();
        }
        public void ChangeBalance(IUser user, double newBalance)
        {
            sQLiteConnection.Open();
            sQLiteCommand.CommandText = $"UPDATE User SET Balance='{newBalance}' WHERE Username='{user.Username}'";
            sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
        }
        public void Dispose()
        {
            sQLiteCommand.Dispose();
            sQLiteConnection.Dispose();
        }
    }
}
