using Microsoft.EntityFrameworkCore;

namespace WebApplication.Model.DB
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:banda-server-db.database.windows.net,1433;" +
                "Initial Catalog=band_db;Persist Security Info=False;" +
                "User ID=banda;Password=12345Sergey;MultipleActiveResultSets=False;" +
                "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}