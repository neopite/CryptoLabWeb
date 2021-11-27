using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [Route("/login")]
    public class LoginController : Controller
    {
        public LoginController()
        {
            //TODO : add db as dependency
        }

        [HttpGet]
        public IActionResult GetLoginPage()
        {
            return View();
        }

        [HttpPost]
        public string Login(LoginInputForm loginInputForm)
        {
            //TODO : 1.validate db hash + password hash from input
            //       2.validate username before passwrod
            return "Page";
        }
    }
}