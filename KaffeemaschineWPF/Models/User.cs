using SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.Models
{
    public class User : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public int Housenumber { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Balance { get; set; }
        public User()
        {
        }
        public User(string firstName, string lastName, string email, string street, int housenumber, string postcode, string city, string username, string password, string balance)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            Housenumber = housenumber;
            Postcode = postcode;
            City = city;
            Username = username;
            Password = password;
            Balance = balance;
        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}