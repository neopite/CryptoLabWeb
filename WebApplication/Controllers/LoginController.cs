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
            var decryptedUser = new UserDbDecryptionHandler().DecryptUserInfoFromDb(userFromDbByUsernameFromForm,key);
            if (string.Equals(userFromDbByUsernameFromForm.Password,
                hashedPassword))
            {
                return "Hello , " + userFromDbByUsernameFromForm.Username + " , city : " + decryptedUser.City;

            }

            return "SMTH WRONG";
        }
    }
}