using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Controllers.Validator;
using WebApplication.Model;

namespace WebApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            // SecretClientOptions options = new SecretClientOptions()
            // {
            //     Retry =
            //     {
            //         Delay= TimeSpan.FromSeconds(2),
            //         MaxDelay = TimeSpan.FromSeconds(16),
            //         MaxRetries = 5,
            //         Mode = RetryMode.Exponential
            //     }
            // };
            // var client = new SecretClient(new Uri("https://band-vault.vault.azure.net/"), new DefaultAzureCredential(),options);
            //
            // KeyVaultSecret secret = client.GetSecret("db-password");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}