using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.Roles.Dto;

namespace JustERP.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, GetRoleInput, CreateRoleDto, RoleDto>, IMetronicTableAppService<RoleDto, GetRoleInput>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
