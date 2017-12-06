using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Charts.Dto
{
    [AutoMapFrom(typeof(LhzxExpert))]
    public class ExpertDto : EntityDto<long>
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
}
