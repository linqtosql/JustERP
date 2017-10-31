using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;

namespace JustERP.OrganizationUnits.Dto
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<long>
    {
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public long? ParentId { get; set; }
        public int MemberCount { get; set; }
    }
}
