using System.ComponentModel.DataAnnotations;
namespace capstone.Models
{
    public class MemberLogin
    {
        [Key]
        [Required(ErrorMessage = "아이디를 입력하세요.")]
        public String userid { get; set; }
        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        [DataType(DataType.Password)]
        public String userpw { get; set; }
    }
}
