using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;
using WebApplication.Model.DB;
using WebApplication.Model.Hashing;

namespace WebApplication.Controllers
{
    [Route("/login")]
    public class LoginController : Controller
    {
        private ApplicationDbContext applicationDbContext;

        public LoginController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetLoginPage()
        {
            return View();
        }

        [HttpPost]
        public string Login(LoginInputForm loginInputForm)
        {
            Console.WriteLine("FEFEFREGREGRE");
            var userFromDbByUsernameFromForm =
                applicationDbContext.User.FirstOrDefault(user => string.Equals(loginInputForm.Username, user.Username));
            if (userFromDbByUsernameFromForm == null)
            {
                return "Invalid username";
            }

            var saltForUsername =
                applicationDbContext.PasswordSalt.FirstOrDefault(x => x.UserId == userFromDbByUsernameFromForm.Id);
            var hashingAlgorithm = new SHA256PasswordHashProvider();
            var hashedPassword = hashingAlgorithm.HashPasswordWithExistingSalt(loginInputForm.Password, saltForUsername.Salt).Hash;
            Console.WriteLine("Password from input : " + loginInputForm.Password);
            Console.WriteLine("SALT : " + saltForUsername.Salt);
            Console.WriteLine("hash from input form : " + hashedPassword);
            Console.WriteLine("From server : " + userFromDbByUsernameFromForm.Password);
            if (string.Equals(userFromDbByUsernameFromForm.Password,
                hashingAlgorithm.HashPasswordWithExistingSalt(loginInputForm.Password, saltForUsername.Salt).Hash))
            {
                return "YEPIII , IT`S CORRECT";
            }
            return "SMTH WRONG";
        }
    }
}