using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpert))]
    public class ExpertDto : EntityDto<long>
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public string ExpertFirstClassName { get; set; }
        public string ExpertClassName { get; set; }
        public int OnlineStatus { get; set; }
    }
}
