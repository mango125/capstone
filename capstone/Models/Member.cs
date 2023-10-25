using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class Member
    {
        [Key]
        [Required(ErrorMessage ="아이디를 입력하세요.")]
        public String userid { get; set; }
        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        [DataType(DataType.Password)]
        public String userpw { get; set; }
        public String name { get; set; }
        public String nickname { get; set; }
        [EmailAddress]
        public String email { get; set; }
        [Phone]
        public String phone { get; set; }
        public String address { get; set; }
        public DateTime joindate { get; set; }

    }
    public class Order_Detail
    {
        public int orderNum { get; set; }
        public String userid { get; set; }
        public int price{ get; set; }
        public int deliveryfee{ get; set; }
        public DateTime orderdate{ get; set; }
        public bool paymentstatus { get; set; }
        public bool deliverystatus { get; set; }
        public DateTime? deliverycompletiondate { get; set; }
        [Key]
        public int orderdetailNum { get; set; }
        public int productNum { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int product_price { get; set; }
    }

}