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
            //여기서 쿼리문 실행할려고 했었는데 뭐 인증이 발행이 안된다 뭐다 어지러워서 일단 여기서 종료함
            Member member = new Member();
            var query = "exec ProcTest";
            var result = _db.Members.FromSqlRaw(query).ToList();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}