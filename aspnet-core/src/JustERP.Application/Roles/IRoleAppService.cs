using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.Roles.Dto;

namespace JustERP.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>, IMetronicTableAppService<RoleDto, MetronicPagedResultRequestDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
