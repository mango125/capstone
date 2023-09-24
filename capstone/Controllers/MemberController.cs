using capstone.DataContext;
using Microsoft.AspNetCore.Mvc;
using capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace capstone.Controllers
{
    public class MemberController : Controller
    {
        private readonly ShoppingmallDbContext _db;
        public MemberController(ShoppingmallDbContext db)
        {
            _db = db;
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
                return View(member);
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
            ////모델의 유효성검사
            //if (ModelState.IsValid)
            //{
            //    FormattableString query = $"exec ProcMemberAdd {member.userid, member.userpw, member.nickname, member.name})";

            //    Member[] Lmember = _db.Member.FromSql(query).ToArray();

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
    }
}
