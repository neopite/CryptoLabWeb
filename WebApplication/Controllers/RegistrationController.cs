using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;
using WebApplication.Model.DB;
using WebApplication.Model.Hashing;

namespace WebApplication.Controllers
{
    [Route("/registration")]
    public class RegistrationController : Controller
    {
        private readonly UserDbContext context;
        public RegistrationController(UserDbContext context)
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
            var user = new User(formInput.Username, hashAlgorithm.HashPasswordWithSalt(formInput.Password, 10).Hash,
                formInput.MobilePhone, formInput.City);
            context.Add(user);
            context.SaveChanges();
            return Redirect("~/login");
        }
    }
}