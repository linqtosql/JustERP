using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;

namespace JustERP.OrganizationUnits.Dto
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<long>
    {
        public string DisplayName { get; set; }
    }
}
