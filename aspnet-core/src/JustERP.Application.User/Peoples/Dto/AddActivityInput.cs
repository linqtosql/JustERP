using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapTo(typeof(MtActivity))]
    public class AddActivityInput
    {
        public long ActivityId { get; set; }
        [Required(ErrorMessage = "请输入活动名称")]
        public string Name { get; set; }
    }

    public class ChangeActivityNameInput : AddActivityInput
    {

    }
}
