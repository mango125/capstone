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
}