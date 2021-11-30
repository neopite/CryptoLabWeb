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
            var userFromDbByUsernameFromForm =
                applicationDbContext.User.FirstOrDefault(user => string.Equals(loginInputForm.Username, user.Username));
            if (userFromDbByUsernameFromForm == null)
            {
                return "Invalid username";
            }

            var saltForUsername =
                applicationDbContext.PasswordSalt.FirstOrDefault(x => x.UserId == userFromDbByUsernameFromForm.Id);
            var hashingAlgorithm = new SHA256PasswordHashProvider();
            var hashedPassword = hashingAlgorithm
                .HashPasswordWithExistingSalt(loginInputForm.Password, saltForUsername.Salt).Hash;
            var dataCypher = new DataCypherSolver();
            var IVforUsername =
                applicationDbContext.IV.FirstOrDefault(x => string.Equals(userFromDbByUsernameFromForm, x.IV));
            var key = configuration["key-data"];
            if (string.Equals(userFromDbByUsernameFromForm.Password,
                hashedPassword))
            {
                return "Hello , " + userFromDbByUsernameFromForm.Username + " , city : " +
                       dataCypher.DecryptStringFromBytes_Aes(Encoding.UTF32.GetBytes(userFromDbByUsernameFromForm.City),
                           Encoding.UTF32.GetBytes(key),
                           Encoding.UTF32.GetBytes(IVforUsername.IV)
                           );
            }

            return "SMTH WRONG";
        }
    }
}