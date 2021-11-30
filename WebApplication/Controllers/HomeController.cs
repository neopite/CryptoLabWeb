using Azure.Security.KeyVault.Keys;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly KeyClient _keyClient;
        public IActionResult Index()
        {
            return View();
            // return _keyClient.GetKey("data");
        }

        [Route("LoginCompleted")]
        public string CompleteRegistration()
        {
            return "Login Successful!";
            // return _keyClient.GetKey("data").Value.ToString();
        }
    }
}