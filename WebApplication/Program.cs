using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Azure;
using WebApplication.Model;
using WebApplication.Model.AES;

// using Azure.Core;
// using Microsoft.Azure.KeyVault;
// using Microsoft.Azure.Services.AppAuthentication;
// using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        { 
             CreateHostBuilder(args).Build().Run();

            // var key = "*F-JaNdRgUkXp2s5";
            // var encr = new UserDbEncryptionHandler();
            // var decr = new UserDbDecryptionHandler();
            // var usr = new FormInput("Serii", "123", "123", "Konotop", "Mobile");
            // var encrypt = encr.Encrypt(usr, key);
            // var decrypt = decr.DecryptUserInfoFromDb(encrypt.user, key);
            // Console.WriteLine(decrypt.City);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        { 
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    var settings = config.Build();
                    var keyVaultEndpoint = "https://band-vault.vault.azure.net/";

                    SecretClientOptions options = new SecretClientOptions();
                    SecretClient client =
                        new SecretClient(new Uri(keyVaultEndpoint), new DefaultAzureCredential(), options);
                    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}