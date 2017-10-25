using Abp.Application.Services.Dto;

namespace JustERP.OrganizationUnits.Dto
{
    public class OrganizationUnitDto : EntityDto<long>
    {
        public string DisplayName { get; set; }
    }
}
