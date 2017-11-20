using System.ComponentModel.DataAnnotations;

namespace JustERP.Application.User.Experts.Dto
{
    public class RegisterDto : RequiredPhoneDto
    {
        [MaxLength(4, ErrorMessage = "手机验证码不能超过4个字符")]
        public string PhoneCode { get; set; }
    }
}
