using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 10)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }


        public override string ToString()
        {
            return String.Format("Id : {0}  Username : {1} Password : {2} PhoneNumber : {3}  Email : {4} ", Id,
                Username, Password, PhoneNumber, Email);
        }
    }
}