using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using capstone.Models;
using capstone.DataContext;

namespace capstone.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShoppingmallDbContext _db;

        public AdminController(ShoppingmallDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductAdd()
        {
            //request 속성값 가져오기
            try
            {
                var requestForm = HttpContext.Request.Form;
                //속성값 변수에 지정하기
                //productName: 상품 이름
                //productImage: 상품 이미지
                //productHtml: 상품 상세설명
                //productValue: 상품 가격
                //productBundleDelivery: 묶음배송 유무
                //productCategory: 상품 카테고리
                //productManufacturer: 상품 제조사

                string productName = requestForm["productName"];
                string productImage = requestForm["productImage"];
                string productHtml = requestForm["productHtml"];
                int productValue = int.Parse(requestForm["productValue"]);
                byte productBundleDelivery = byte.Parse(requestForm["productBundleDelivery"]);
                string productCategory = requestForm["productCategory"];
                string productManufacturer = requestForm["productManufacturer"];



                //카테고리 별 메서드 지정
                switch (productCategory)
                {
                    case "mainboard":
                        // 제품명(mainboardName) 
                        // CPU소켓(mainboardCpuSocket) 
                        // 칩셋(mainboardChipset)
                        // 폼펙터(mainboardFormfactor) 
                        // 메모리 종류(mainboardRamSocket)
                        // 메모리 슬롯(mainboardMemorySlots)
                        string mainboardName = requestForm["mainboardName"];
                        string mainboardCpuSocket = requestForm["mainboardCpuSocket"];
                        string mainboardChipset = requestForm["mainboardChipset"];
                        string mainboardFormfactor = requestForm["mainboardFormfactor"];
                        string mainboardRamSocket = requestForm["mainboardRamSocket"];
                        int mainboardMemorySlots = int.Parse(requestForm["mainboardMemorySlots"]);

                        //카테고리, 상품명, 제조사, 가격, 묶음배송, 판매유무, 품절유무, 이미지, 메인페이지
                        //메인보드 명, CPU소켓, 메인보드칩셋, 메인보드폼펙터, 램소켓, 메모리슬롯
                        _db.Database.ExecuteSqlRaw("exec TranProductMainboardAdd {0}, {1}, {2}, {3},{4}, 1,1,{5},{6},{7},{8},{9},{10},{11},{12}",
                            productCategory, productName, productManufacturer, productValue, productBundleDelivery, productImage, productHtml,
                            mainboardName, mainboardCpuSocket, mainboardChipset, mainboardFormfactor, mainboardRamSocket, mainboardMemorySlots
                            );
                        break;
                    default:
                        break;
                }

                return View();
            }

            catch
            {
                return View();
            }
        }
    }
}
