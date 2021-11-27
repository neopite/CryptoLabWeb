using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public User(string username, string password, string phoneNumber, string email)
        {
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public override string ToString()
        {
            return String.Format("Id : {0}  Username : {1} Password : {2} PhoneNumber : {3}  Email : {4} ", Id,
                Username, Password, PhoneNumber, Email);
        }
    }
}