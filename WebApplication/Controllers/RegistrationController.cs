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
            if (!ModelState.IsValid)
            {
                if (!string.Equals(formInput.Password, formInput.PasswordConfirm))
                {
                    ModelState.AddModelError("password", "Password and Confirm password not the same");
                }

                return Redirect("~/registration");
            }
            IUserDbEncryptionHandler handler = new UserDbEncryptionHandler();
            var encryptedRow = handler.Encrypt(formInput, key);
            context.User.Add(encryptedRow.user);
            context.SaveChanges();
            var userId = context.User.FirstOrDefault(x => string.Equals(encryptedRow.user.Username, x.Username)).Id;
            context.PasswordSalt.Add(new PasswordSalt(userId,encryptedRow.password.Salt));
            context.IV.Add(new InitVector(userId, BitConverter.ToString(encryptedRow.IV)));
            context.SaveChanges();
            return Redirect("~/login");
        }
    }
}