using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Model;
using WebApplication.Model.DB;
using WebApplication.Model.Entety;
using WebApplication.Model.Hashing;

namespace WebApplication.Controllers
{
    [Route("/registration")]
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext context;
        public RegistrationController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public IActionResult GetRegistrationPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrateUser(FormInput formInput)
        {
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
                formInput.MobilePhone, formInput.City);
            context.User.Add(user);
            context.SaveChanges();
            var userId = context.User.FirstOrDefault(x => string.Equals(user.Username, x.Username)).Id;
            context.PasswordSalt.Add(new PasswordSalt(userId,saltedPassword.Salt));
            context.SaveChanges();
            return Redirect("~/login");
        }
    }
}