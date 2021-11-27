using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        { 
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
            // return Host.CreateDefaultBuilder(args)
            //     .ConfigureWebHostDefaults(webBuilder =>
            //         webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            //             {
            //                 var settings = config.Build();
            //
            //                 config.AddAzureAppConfiguration(options =>
            //                 {
            //                     options.Connect(settings["ConnectionStrings:AppConfig"])
            //                         .ConfigureKeyVault(kv =>
            //                         {
            //                             kv.SetCredential(new DefaultAzureCredential());
            //                         });
            //                 });
            //             })
            //             .UseStartup<Startup>());
        }
    }
}

// using System;
// using System.Data.SqlClient;
//
// using Azure.Identity;
// class Program
//     {
//         static void Main(string[] args)
//         {
//             try 
//             { 
//                 SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
//                 builder.ConnectionString="Server=tcp:banda-server-db.database.windows.net,1433;" +
//                                          "Initial Catalog=band_db;Persist Security Info=False;" +
//                                          "User ID=banda;Password=12345Sergey;MultipleActiveResultSets=False;" +
//                                          "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
//
//                 using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
//                 {
//                     Console.WriteLine("\nQuery data example:");
//                     Console.WriteLine("=========================================\n");
//                     
//                     connection.Open();       
//
//                     String sql = "SELECT name, collation_name FROM sys.databases";
//
//                     using (SqlCommand command = new SqlCommand(sql, connection))
//                     {
//                         using (SqlDataReader reader = command.ExecuteReader())
//                         {
//                             while (reader.Read())
//                             {
//                                 Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
//                             }
//                         }
//                     }                    
//                 }
//             }
//             catch (SqlException e)
//             {
//                 Console.WriteLine(e.ToString());
//             }
//             Console.WriteLine("\nDone. Press enter.");
//             Console.ReadLine(); 
//         }
//     }