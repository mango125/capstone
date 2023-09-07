using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
