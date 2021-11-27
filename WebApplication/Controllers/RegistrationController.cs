using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;

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
        public IActionResult  RegistrateUser(FormInput formInput)
        {
            if (!ModelState.IsValid)
            {
                if (!string.Equals(formInput.Password, formInput.PasswordConfirm))
                {
                    ModelState.AddModelError("password","Password and Confirm password not the same");
                }
                return Redirect("~/registration");
            }
            return Redirect("~/LoginCompleted");
        }
    }
}