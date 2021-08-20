using System;

namespace SharedObjects
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Street { get; set; }
        int Housenumber { get; set; }
        string Postcode { get; set; }
        string City { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}