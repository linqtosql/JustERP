using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.OrganizationUnits.Dto;

namespace JustERP.OrganizationUnits
{
    public interface IOrganizationUnitAppService : IAsyncCrudAppService<OrganizationUnitDto, long, PagedResultRequestDto, CreateOrganizationUnitDto, OrganizationUnitDto>
    {
        Task<List<OrganizationUnitDto>> GetOrganizationUnits();
    }
}