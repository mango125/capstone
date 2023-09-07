using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace capstone.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult ProductInfo()
        {
            return View();
        }
        public IActionResult B660M()
        {
            return View();
        }

    }
}
