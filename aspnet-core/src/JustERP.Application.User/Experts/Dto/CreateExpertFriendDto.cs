using System.ComponentModel.DataAnnotations;

namespace JustERP.Application.User.Experts.Dto
{
    public class CreateExpertFriendDto
    {
        public long ExpertId { get; set; }
        [Required(ErrorMessage = "请输入手机号")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请输入姓名")]
        public string Name { get; set; }
    }
}
