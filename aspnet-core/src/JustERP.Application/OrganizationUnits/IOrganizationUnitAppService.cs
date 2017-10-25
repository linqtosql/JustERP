using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.OrganizationUnits.Dto;

namespace JustERP.OrganizationUnits
{
    public interface IOrganizationUnitAppService : IAsyncCrudAppService<OrganizationUnitDto, long, PagedResultRequestDto, CreateOrganizationUnitDto, OrganizationUnitDto>, IMetronicTableAppService<OrganizationUnitDto, MetronicPagedResultRequestDto>
    {

    }
}