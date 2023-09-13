using capstone.DataContext;
using Microsoft.EntityFrameworkCore;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace capstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoppingmallDbContext _db;
        public HomeController(ShoppingmallDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(); // 또는 오류 페이지로 리디렉션
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Select()
        {
            var member = new Member();
            var query = _db.Member.FromSqlRaw("exec ProcText").ToList();
            member = query.SingleOrDefault();
            return View("Index",member);
        }

        public IActionResult ChkLogin()
        {
            var member = new Member();
            var query = _db.Member.FromSqlRaw("exec ProcText").ToList();
            member = query.SingleOrDefault();
            return View("Index", member);
        }
    }
}