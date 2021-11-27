using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;
using WebApplication.Model.Hashing;

namespace WebApplication.Controllers
{
    [Route("/registration")]
    public class RegistrationController : Controller
    {
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
                formInput.Email, formInput.Email);
            //TODO : create new user and add salt to user table or another
            return Redirect("~/login");
        }
    }
}