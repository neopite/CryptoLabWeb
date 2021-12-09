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
            //CreateHostBuilder(args).Build().Run();
            var key = "*F-JaNdRgUkXp2s5";
            var inputForm = new FormInput("1","123123123123","123123123123","2","4");
            var userDbEcnryptor = new UserDbEncryptionHandler();
            var userDbDecryptorHandler = new UserDbDecryptionHandler();
            var encrypted = userDbEcnryptor.Encrypt(inputForm, key);
            var decrypted = userDbDecryptorHandler.DecryptUserInfoFromDb(encrypted.user, key);
            Console.WriteLine(decrypted.City);
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