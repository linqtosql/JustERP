using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.Roles.Dto;
using JustERP.Users.Dto;

namespace JustERP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>, IMetronicTableAppService<UserDto, MetronicPagedResultRequestDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}