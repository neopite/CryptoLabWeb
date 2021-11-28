using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using WebApplication.Model.Entety;

namespace WebApplication.Model.DB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User {get; set; }
        public DbSet<PasswordSalt> PasswordSalt { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };
            var client = new SecretClient(new Uri("https://band-vault.vault.azure.net/"), 
                new DefaultAzureCredential(),options);
            
            KeyVaultSecret secret_login = client.GetSecret("db-login");
            KeyVaultSecret secret_password = client.GetSecret("db-password");
            
            optionsBuilder.UseSqlServer(@"Server=tcp:banda-server-db.database.windows.net,1433;" +
                "Initial Catalog=band_db;Persist Security Info=False;" +
                $"User ID=banda;Password=12345Sergey;MultipleActiveResultSets=False;" +
                "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }*/
    }
}