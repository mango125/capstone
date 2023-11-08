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
using Azure.Core;

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

            string whereQuery = "";
            string SortBy;

            //공통select로 들어갈 구문
            //제조사
            if (request.Query["manufacturer"].Any())
            {
                string[] Selectedmanufacturer;
                Selectedmanufacturer = request.Query["manufacturer"].ToArray();
                foreach(string manufacturer in Selectedmanufacturer)
                {
                    whereQuery += " productManufacturer = '" + manufacturer + "' OR ";
                }
            }
            //정렬
            if (request.Query["sort"] == "REV")
            {
                SortBy = "ORDER BY REVIEW_COUNTER DESC";
            }
            else
            {
                SortBy = "ORDER BY productPrice " + request.Query["sort"].ToString();
            }

            //카테고리가 메인보드일 경우
            if (categoryId == "mainboard") 
            {
                //태그입력 시 실행               
                whereQuery += CheckTags(request, "mainboardCpuSocket", "mainboardCpuSocket"); //CPU소켓
                whereQuery += CheckTags(request, "mainboardFormfactor", "mainboardFormfactor"); //폼펙터
                whereQuery += CheckTags(request, "mainboardRamSocket", "mainboardRamSocket"); //렘 소켓
                //메모리 슬롯 수
                string memorySlots = "";
                if (request.Query["mainboardMemorySlots"].Any())
                {
                    memorySlots = "mainboardMemorySlots = " + request.Query["mainboardMemorySlots"];
                }
                //조건식 만약 whereQuery에 값이 있을 때 앞에 where을 추가
                //또한 memorySlots에 값이 있을 때 whereQuery 앞 뒤에 괄호와 AND 추가
                if (whereQuery.Any())
                {
                    //맨 뒤의 OR을 제거
                    whereQuery = whereQuery.Substring(0, whereQuery.Length - 3);
                    //메모리 슬롯에 값이 있을 경우
                    if (memorySlots.Any())
                    {
                        whereQuery = " WHERE (" + whereQuery + ") AND" + memorySlots;
                    }
                    else
                    {
                        whereQuery = " WHERE " + whereQuery;
                    }
                    //formattablestring 형태의 변수 사용 시 where구문을 제대로 인식하지 못하여 string형태의 변수를 이용
                    string strQuery = $@"select * from View_product_mainboard {whereQuery}";

                    Product_Mainboard[] product_Mainboards = _db.Product_Mainboard.FromSqlRaw(strQuery).ToArray();
                    ViewBag.productInfo = product_Mainboards;
                    return View();

                }
                else
                {
                    query = $"select * from View_product_mainboard";
                    Product_Mainboard[] product_Mainboards = _db.Product_Mainboard.FromSql(query).ToArray();
                    ViewBag.productInfo = product_Mainboards;
                }

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

        //태그값을 확인하는 메서드
        //tag = query문에 직접 입력될 속성 명
        //value = request에서 속성을 찾을 때 사용될 속성 명
        public string CheckTags(HttpRequest request, string tag, string value)
        {
            string whereQuery = "";
            string[] SelectedQuery = request.Query[value];
            if (SelectedQuery.Any())
            {
                foreach (string selectedValue in SelectedQuery)
                {
                    whereQuery += tag + " = '" + selectedValue + "' OR ";
                }
            }
            return whereQuery;
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
