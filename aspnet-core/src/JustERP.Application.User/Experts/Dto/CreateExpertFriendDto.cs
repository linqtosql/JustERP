using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapTo(typeof(LhzxExpertAnonymousShip))]
    public class CreateExpertFriendDto
    {
        public long ExpertId { get; set; }
        [MaxLength(11, ErrorMessage = "请输入正确的手机号")]
        [Required(ErrorMessage = "请输入手机号")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请输入姓名")]
        public string Name { get; set; }
    }
}
