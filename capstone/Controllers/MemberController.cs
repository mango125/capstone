using capstone.DataContext;
using Microsoft.AspNetCore.Mvc;
using capstone.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics.Metrics;

namespace capstone.Controllers
{
    public class MemberController : Controller
    {
        private readonly ShoppingmallDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor; //URL정보를 읽기위한 변수
        public MemberController(ShoppingmallDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor; 
        }
        public IActionResult Index()
        {
            //로그인이 되어있지 않으면 로그인 뷰 리턴
            if (HttpContext.Session.GetString("userid")==null)
            {
                return View("Login");
            }
            else
            {
                //로그인 되어있으면 사용자 정보를 읽어오고 마이페이지로 이동
                Member member = new Member();
                member.userid = HttpContext.Session.GetString("userid").ToString(); //세션에 존재하는 userid문자열을 가져옴
                FormattableString query = $"exec ProcMemberChk {member.userid}";
                member = (Member)_db.Member.FromSql(query).ToList()[0];
                ViewBag.Member = member;

                query = $"SELECT * FROM View_Order_Detail WHERE userid = {member.userid}";
                Order_Detail[] orders = _db.Order_Detail.FromSql(query).ToArray();

                ViewBag.Orders = orders;
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        //로그인 처리의 경우 Post방식으로 처리해야 함
        [HttpPost]
        public IActionResult LoginProc(MemberLogin memberLogin) 
        {
            //모델의 유효성검사
            if (ModelState.IsValid)
            {
                FormattableString query = $"exec ProcMemberChk {memberLogin.userid}";

                Member[] member = _db.Member.FromSql(query).ToArray() ;

                if(member.Length ==1) //멤버가 있을 경우
                {
                    if (member[0].userpw.Trim()==memberLogin.userpw.Trim()) //비밀번호가 맞을경우
                    {
                        //로그인 처리
                        HttpContext.Session.SetString("userid", member[0].userid);
                        ViewBag.Member = member[0]; //ViewBag에 불러온 멤버의 정보 입력
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        //비밀번호 틀린경우
                        ViewData["Error"] = "비밀번호가 일치하지 않습니다.";
                        return View("Login", memberLogin);
                    }
                }
                else
                {
                    //존재하지 않는 아이디
                    ViewData["Error"] = "비밀번호가 일치하지 않습니다.";
                    return View("Login", memberLogin);
                }
            }
            else
            {
                //아이디, 비밀번호 미입력
                ViewData["Error"] = "아이디와 비밀번호를 입력해주세요.";
                return View("Login", memberLogin);
            }
        }
        public IActionResult Registor(Member member)
        {
            //모델의 유효성검사
            //if (ModelState.IsValid)
            //{
            //    FormattableString query = $"exec ProcMemberAdd {member.userid,member.userpw, member.nickname, member.name})";

            //    Member[] member = _db.Member.FromSql(query).ToArray();

            //    if (member.Length == 1) //멤버가 있을 경우
            //    {
            //        if (member[0].userpw.Trim() == memberLogin.userpw.Trim()) //비밀번호가 맞을경우
            //        {
            //            //로그인 처리
            //            HttpContext.Session.SetString("userid", member[0].userid);
            //            ViewBag.Member = member[0]; //ViewBag에 불러온 멤버의 정보 입력
            //            return RedirectToAction("Index", "Home");
            //        }
            //        else
            //        {
            //            //비밀번호 틀린경우
            //            ViewData["Error"] = "비밀번호가 일치하지 않습니다.";
            //            return View("Login", memberLogin);
            //        }
            //    }
            //    else
            //    {
            //        //존재하지 않는 아이디
            //        ViewData["Error"] = "비밀번호가 일치하지 않습니다.";
            //        return View("Login", memberLogin);
            //    }
            //}
            //else
            //{
            //    //아이디, 비밀번호 미입력
            //    ViewData["Error"] = "아이디와 비밀번호를 입력해주세요.";
            //    return View("Login", memberLogin);
            //}

            return View();
        }
        public IActionResult Logout(Member memberLogin)
        {
            //세션 초기화
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Member");
        }


        //상품 구매 페이지관련 메서드
        public IActionResult Checkout(string[] productNum)
        {
            //로그인 되어있어야 상품구매 가능함
            if (HttpContext.Session.GetString("userid") == null)
            {
                return View("Login");
            }
            else
            {
                string whereQuery = "";

                // 매개변수로 가져온 productNum을 where절에 넣기위한 과정
                for (int i = 0; i < productNum.Length; i++)
                {
                    string paramName = productNum[i];
                    whereQuery += $"productNum = '{paramName}'";

                    // 마지막 반복에서는 OR 연산자를 추가하지 않음
                    if (i < productNum.Length - 1)
                    {
                        whereQuery += " OR ";
                    }

                }

                //FormattableString query = $"SELECT * FROM product_info WHERE {whereQuery}";
                //ProductInfo[] ProductInfo = _db.ProductInfo.FromSql(query).ToArray();

                FormattableString query = $"SELECT * FROM product_info WHERE productNum = {productNum[0]}";
                ProductInfo[] ProductInfo = _db.ProductInfo.FromSql(query).ToArray();
                ViewBag.productInfo = ProductInfo;

                //사용자 정보를 읽어옴
                Member member = new Member();
                member.userid = HttpContext.Session.GetString("userid").ToString(); //세션에 존재하는 userid문자열을 가져옴
                query = $"exec ProcMemberChk {member.userid}";
                member = (Member)_db.Member.FromSql(query).ToList()[0];
                ViewBag.Member = member;


                return View("Checkout");
            }
        }

        //구매를 확정짓는 메서드
        public IActionResult CheckoutComplete()
        {
            //상품의 정보를 가져옴
            var request = _httpContextAccessor.HttpContext.Request; //여기서 구매한 상품번호, 금액, 수량을 가져옴
            var productNum = Convert.ToInt32(request.Query["productNum"]); //상품번호
            var productPrice = Convert.ToInt32(request.Query["price"]); //금액

            var userid = HttpContext.Session.GetString("userid").ToString(); //세션에 존재하는 userid문자열을 가져옴


            _db.Database.ExecuteSqlRaw("exec ProcOrderAdd {0}, {1}, 1", userid, productPrice);
            _db.Database.ExecuteSqlRaw("exec ProcOrderDetailAdd {0}, {1}, 1", userid, Convert.ToInt32(productNum));

            //사용자 정보를 읽어옴
            FormattableString query = $"exec ProcMemberChk {userid}";
            Member[] member = _db.Member.FromSql(query).ToArray();
            ViewBag.Member = member;
            return View("Index");
        }
    }
}
