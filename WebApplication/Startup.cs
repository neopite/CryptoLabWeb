using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Controllers.Validator;
using WebApplication.Model;
using WebApplication.Model.DB;

namespace WebApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var secret_login = _configuration["db-login"];
            var secret_password = _configuration["db-password"];
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("Server=tcp:banda-server-db.database.windows.net,1433;" +
                                                "Initial Catalog=band_db;Persist Security Info=False;" +
                                                $"User ID={secret_login};Password={secret_password};MultipleActiveResultSets=False;" +
                                                "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var client = new KeyClient(vaultUri: new Uri("https://band-vault.vault.azure.net/"), credential: new DefaultAzureCredential());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    async context => { await context.Response.WriteAsync($"The secret value is: {client.GetKey("key-name")}"); });
            });

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home}/{action=Index}/{id?}");
            //     
            // });
        }
    }
}