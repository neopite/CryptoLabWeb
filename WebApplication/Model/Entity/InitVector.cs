using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Model.Entity
{
    public class InitVector
    {
        [Key]
        [ForeignKey("Id")]
        public int UserId { get; private set; }
        public string IV { get; private set; }

        public InitVector(int userId, string IV)
        {
            UserId = userId;
            this.IV = IV;
        }
    }
}