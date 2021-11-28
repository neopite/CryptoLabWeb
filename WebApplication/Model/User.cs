using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string MobilePhone { get; set; }
        public string City { get; set; }

        public User(string username, string password, string mobilePhone, string city)
        {
            Username = username;
            Password = password;
            MobilePhone = mobilePhone;
            City = city;
        }

        public override string ToString()
        {
            return String.Format("Id : {0}  Username : {1} Password : {2} PhoneNumber : {3}  Email : {4} ", Id,
                Username, Password, MobilePhone, City);
        }
    }
}