using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication.Model;
using WebApplication.Model.AES;
using WebApplication.Model.DB;
using WebApplication.Model.Hashing;

namespace WebApplication.Controllers
{
    [Route("/login")]
    public class LoginController : Controller
    {
        private ApplicationDbContext applicationDbContext;
        private readonly IConfiguration configuration;


        public LoginController(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetLoginPage()
        {
            return View();
        }

        [HttpPost]
        public string Login(LoginInputForm loginInputForm)
        {
            var key = configuration["data-k"];
            var userFromDbByUsernameFromForm =
                applicationDbContext.User.FirstOrDefault(user => string.Equals(loginInputForm.Username, user.Username));
            if (userFromDbByUsernameFromForm == null)
            {
                return "Invalid username";
            }

            var saltForUsername =
                applicationDbContext.PasswordSalt.FirstOrDefault(x => x.UserId == userFromDbByUsernameFromForm.Id);
            var hashingAlgorithm = new Argon2PasswordHashProvider();
            var hashedPassword = hashingAlgorithm
                .HashPasswordWithExistingSalt(loginInputForm.Password, saltForUsername.Salt).Hash;
            var dataCypher = new DataCypherSolver();
            var IVforUsername =
                applicationDbContext.IV.FirstOrDefault(x => string.Equals(userFromDbByUsernameFromForm.Id, x.UserId));
            if (string.Equals(userFromDbByUsernameFromForm.Password,
                hashedPassword))
            {
                return "Hello , " + userFromDbByUsernameFromForm.Username + " , city : " +
                      dataCypher.DecryptStringFromBytes_Aes(Convert.FromBase64String(userFromDbByUsernameFromForm.City),
                           Encoding.ASCII.GetBytes(key),
                           IVforUsername.IV.Split('-').Select(b => Convert.ToByte(b, 16)).ToArray()
                           );
            }

            return "SMTH WRONG";
        }
    }
}