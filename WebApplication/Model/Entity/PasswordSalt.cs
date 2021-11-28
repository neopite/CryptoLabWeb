using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Model.Entety
{
    public class PasswordSalt
    {
        [Key]
        [ForeignKey("Id")]
        public int UserId { get; private set; }
        public string Salt { get; private set; }

        public PasswordSalt(int userId, string salt)
        {
            UserId = userId;
            Salt = salt;
        }
    }
}