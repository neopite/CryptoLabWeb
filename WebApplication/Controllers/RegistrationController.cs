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
        public IActionResult  RegistrateUser(User user)
        {
            Console.WriteLine(user.ToString());
            if (!ModelState.IsValid)
            {
                if (!string.Equals(user.Password, user.PasswordConfirm))
                {
                    ModelState.AddModelError("password","Password and Confirm password not the same");
                    Console.WriteLine("fesfergergerger");
                }
                return Redirect("~/registration");
            }
            return Redirect("~/LoginCompleted");
        }
    }
}