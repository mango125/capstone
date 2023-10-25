using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductAdd()
        {
            return View();
        }
    }
}
