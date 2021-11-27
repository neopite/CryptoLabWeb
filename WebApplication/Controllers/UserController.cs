using System.Timers;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        [Route("user")]
        public string GetUserByID()
        {
            return "sraka" + new Timer().Interval;
        }
    }
}