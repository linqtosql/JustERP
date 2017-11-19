using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertClass))]
    public class ExpertClassDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Background { get; set; }
        public List<ExpertClassDto> ChildrenExpertClasses { get; set; }
        public List<ExpertDto> Experts { get; set; }
    }
}
