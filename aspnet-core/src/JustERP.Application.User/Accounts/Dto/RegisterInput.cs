using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Accounts.Dto
{
    [AutoMapTo(typeof(LhzxExpertAccount))]
    public class RegisterInput
    {
        [Required(ErrorMessage = "请输入您的手机号")]
        [MaxLength(16)]
        public string UserName { get; set; }
        [MaxLength(4, ErrorMessage = "手机验证码不能超过4个字符")]
        public string PhoneCode { get; set; }
    }
}
