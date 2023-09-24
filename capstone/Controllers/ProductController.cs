using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using capstone.Models;
using capstone.DataContext;
using System.Diagnostics.Metrics;
using Microsoft.Identity.Client;

namespace capstone.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingmallDbContext _db;
        public ProductController(ShoppingmallDbContext db)
        {
            _db = db;
        }
        public IActionResult CategoryList(string categoryId)
        {
            
            if (categoryId == "mainboard") 
            {
                FormattableString query = $"select * from View_product_mainboard where productCategory = 'mainboard'";
                //FormattableString query = $"select * from product_info";
                Product_Mainboard[] product_Mainboards = _db.Product_Mainboard.FromSql(query).ToArray();
                //ProductInfo[] productinfo = _db.ProductInfo.FromSql(query).ToArray();

                ViewBag.productInfo = product_Mainboards;
                //ViewBag.productInfo = productinfo;
            }
            else if (categoryId == "CPU")
            {
                FormattableString query = $"select * from product_info";
                ProductInfo[] productinfo = _db.ProductInfo.FromSql(query).ToArray();

                ViewBag.productInfo = productinfo;
            }

            

            
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
