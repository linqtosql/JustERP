using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpert))]
    public class LoggedInExpertOutput : EntityDto<long>
    {
        public bool IsExpert { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public int OnlineStatus { get; set; }
    }
}
