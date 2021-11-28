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
    }
}