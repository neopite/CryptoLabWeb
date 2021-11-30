using Azure.Security.KeyVault.Keys;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("LoginCompleted")]
        public string CompleteRegistration()
        {
            return "Login Successful!";
        }
    }
}