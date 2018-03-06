using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.Zero.Configuration;
using JustERP.Authorization;
using JustERP.OrganizationUnits.Dto;

namespace JustERP.OrganizationUnits
{
    [AbpAuthorize(PermissionNames.Pages_OrganizationUnits)]
    public class OrganizationUnitAppService : AsyncCrudAppService<OrganizationUnit, OrganizationUnitDto, long, PagedResultRequestDto, CreateOrganizationUnitDto, OrganizationUnitDto>, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private IRepository<UserOrganizationUnit, long> _userOrgRepository;
        public OrganizationUnitAppService(
            IRepository<OrganizationUnit, long> repository,
            IRepository<UserOrganizationUnit, long> userOrgRepository,
            OrganizationUnitManager organizationUnitManager) : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
            _userOrgRepository = userOrgRepository;
        }

        public override async Task<OrganizationUnitDto> Create(CreateOrganizationUnitDto input)
        {
            var orgazitionUnit = ObjectMapper.Map<OrganizationUnit>(input);
            await _organizationUnitManager.CreateAsync(orgazitionUnit);
            return ObjectMapper.Map<OrganizationUnitDto>(orgazitionUnit);
        }

        public async Task<List<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var units = ObjectMapper.Map<List<OrganizationUnitDto>>(await _organizationUnitManager.FindChildrenAsync(null, true));
            foreach (var organizationUnitDto in units)
            {
                organizationUnitDto.MemberCount = _userOrgRepository.Count(uou => uou.OrganizationUnitId == organizationUnitDto.Id);
            }
            return units;
        }
    }
}
