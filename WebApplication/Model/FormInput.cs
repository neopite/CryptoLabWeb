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

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}