using System.ComponentModel.DataAnnotations;

namespace JustERP.Application.User.Experts.Dto
{
    public class RequiredPhoneDto
    {
        [Required(ErrorMessage = "请输入您的手机号")]
        [MaxLength(16)]
        public string Phone { get; set; }
    }
}
