using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using capstone.Models;
using capstone.DataContext;
using System.Diagnostics.Metrics;
using Microsoft.Identity.Client;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Components;

namespace capstone.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingmallDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor; //URL정보를 읽기위한 변수

        public ProductController(ShoppingmallDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult CategoryList()
        {
            var request = _httpContextAccessor.HttpContext.Request; //URL정보를 읽기위한 변수
            var categoryId = request.Query["category"];
            FormattableString query;

            if (categoryId[0] == "mainboard") 
            {
                var Selectedmanufacturer = request.Query["manufacturer"].ToArray();
                if(Selectedmanufacturer.Any())
                {
                    query = $"select * from View_product_mainboard where productManufacturer = {Selectedmanufacturer}";
                }
                else
                {
                    query = $"select * from View_product_mainboard";
                }
                Product_Mainboard[] product_Mainboards = _db.Product_Mainboard.FromSql(query).ToArray();
                ViewBag.productInfo = product_Mainboards;

            }
            else if (categoryId == "CPU")
            {
                //    FormattableString query = $"select * from View_product_CPU where productCategory = 'CPU'";
                //    Product_CPU[] productCPUs = _db.Product_CPU.FromSql(query).ToArray();
                //    ViewBag.productInfo = productCPUs;
                ViewBag.productInfo.productCategory = "CPU";
            //    return View();
            }
            else if (categoryId == "VGA")
            {
                //    FormattableString query = $"select * from View_product_VGA where productCategory = 'VGA'";
                //    Product_VGA[] productVGAs = _db.Product_VGA.FromSql(query).ToArray();
                //    ViewBag.productInfo = productVGAs;
                ViewBag.productInfo = "VGA";
                //    return View();
            }
            else if (categoryId == "case")
            {
                //    FormattableString query = $"select * from View_product_case where productCategory = 'case'";
                //    Product_Case[] productCases = _db.Product_Case.FromSql(query).ToArray();
                //    ViewBag.productInfo = productCases;
                ViewBag.productInfo = "case";
                //    return View();
            }
            else if (categoryId == "ram")
            {
                //    FormattableString query = $"select * from View_product_ram where productCategory = 'ram'";
                //    Product_RAM[] productRAMs = _db.Product_RAM.FromSql(query).ToArray();
                //    ViewBag.productInfo = productRAMs;
                ViewBag.productInfo = "ram";
                //    return View();
            }
            else if (categoryId == "SSD")
            {
                //    FormattableString query = $"select * from View_product_SSD where productCategory = 'SSD'";
                //    Product_SSD[] productSSDs = _db.Product_SSD.FromSql(query).ToArray();
                //    ViewBag.productInfo = productSSDs;
                ViewBag.productInfo = "SSD";
                //    return View();
            }
            else if (categoryId == "HDD")
            {
                //    FormattableString query = $"select * from View_product_HDD where productCategory = 'HDD'";
                //    Product_HDD[] productHDDs = _db.Product_HDD.FromSql(query).ToArray();
                //    ViewBag.productInfo = productHDDs;
                ViewBag.productInfo = "HDD";
                //    return View();
            }
            return View();
        }


        [HttpGet]
        public IActionResult ProductInfo(string category, int productNum)
        {
            FormattableString query;
            query = $"select * from View_product_mainboard where productNum = {productNum}";
            Product_Mainboard[] product_Mainboards = _db.Product_Mainboard.FromSql(query).ToArray();
            ViewBag.productInfo = product_Mainboards;

            return View();
        }

        public IActionResult CategoryMainboard(string[] manufacturer, string[] mainboardCpuSocket, string[] mainboardFormfactor, string[] mainboardRamSocket, int mainboardMemorySlots, string sort, string productname)
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
