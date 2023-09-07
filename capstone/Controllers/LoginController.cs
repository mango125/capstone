using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registor()
        {
            return View();
        }
    }
}
