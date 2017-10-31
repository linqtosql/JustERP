using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Organizations;
using JustERP.Authorization;
using JustERP.OrganizationUnits.Dto;

namespace JustERP.OrganizationUnits
{
    [AbpAuthorize(PermissionNames.Pages_OrganizationUnits)]
    public class OrganizationUnitAppService : AsyncCrudAppService<OrganizationUnit, OrganizationUnitDto, long, PagedResultRequestDto, CreateOrganizationUnitDto, OrganizationUnitDto>, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        public OrganizationUnitAppService(
            IRepository<OrganizationUnit, long> repository,
            OrganizationUnitManager organizationUnitManager) : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
        }

        public override async Task<OrganizationUnitDto> Create(CreateOrganizationUnitDto input)
        {
            var orgazitionUnit = ObjectMapper.Map<OrganizationUnit>(input);
            await _organizationUnitManager.CreateAsync(orgazitionUnit);
            return ObjectMapper.Map<OrganizationUnitDto>(orgazitionUnit);
        }

        public async Task<List<OrganizationUnitDto>> GetOrganizationUnits()
        {
            return ObjectMapper.Map<List<OrganizationUnitDto>>(await _organizationUnitManager.FindChildrenAsync(null));
        }
    }
}
