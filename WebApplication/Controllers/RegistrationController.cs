using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication.Model;
using WebApplication.Model.AES;
using WebApplication.Model.DB;
using WebApplication.Model.Entety;
using WebApplication.Model.Entity;
using WebApplication.Model.Hashing;

namespace WebApplication.Controllers
{
    [Route("/registration")]
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        public RegistrationController(ApplicationDbContext context , IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        
        [HttpGet]
        public IActionResult GetRegistrationPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrateUser(FormInput formInput)
        {
            var key = configuration["data-k"];
            var dataCypher = new DataCypherSolver();
            var IV = dataCypher.GetIV();
            var byteKey = Encoding.UTF32.GetBytes(key);
            if (!ModelState.IsValid)
            {
                if (!string.Equals(formInput.Password, formInput.PasswordConfirm))
                {
                    ModelState.AddModelError("password", "Password and Confirm password not the same");
                }

                return Redirect("~/registration");
            }
            
            var hashAlgorithm = new SHA256PasswordHashProvider();
            var saltedPassword = hashAlgorithm.HashPasswordWithSalt(formInput.Password, 10);
            var user = new User(formInput.Username, saltedPassword.Hash,
                dataCypher.Encrypt(formInput.MobilePhone,byteKey,IV), dataCypher.Encrypt(formInput.City,byteKey,IV));
            context.User.Add(user);
            context.SaveChanges();
            var userId = context.User.FirstOrDefault(x => string.Equals(user.Username, x.Username)).Id;
            context.PasswordSalt.Add(new PasswordSalt(userId,saltedPassword.Salt));
            context.IV.Add(new InitVector(userId, Encoding.ASCII.GetString(IV)));
            context.SaveChanges();
            return Redirect("~/login");
        }
    }
}