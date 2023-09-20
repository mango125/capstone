using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using capstone.Models;
using capstone.DataContext;
using System.Diagnostics.Metrics;

namespace capstone.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingmallDbContext _db;
        public ProductController(ShoppingmallDbContext db)
        {
            _db = db;
        }
        public IActionResult CategoryList()
        {
            FormattableString query = $"select * from product_info";
            ProductInfo[] productinfo = _db.ProductInfo.FromSql(query).ToArray();

            ViewBag.productInfo = productinfo;

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
