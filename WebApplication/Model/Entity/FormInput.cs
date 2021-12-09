using System.ComponentModel.DataAnnotations;

namespace WebApplication.Model
{
    public class FormInput
    {
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "", MinimumLength = 10)]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password", ErrorMessage = "Passwords not the same")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public string MobilePhone { get; set; }

        public string City { get; set; }

        public FormInput(string username, string password, string passwordConfirm, string mobilePhone, string city)
        {
            Username = username;
            Password = password;
            PasswordConfirm = passwordConfirm;
            MobilePhone = mobilePhone;
            City = city;
        }
    }
}