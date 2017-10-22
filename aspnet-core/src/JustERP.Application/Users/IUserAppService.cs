using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.Roles.Dto;
using JustERP.Users.Dto;

namespace JustERP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task<MetronicPagedResultDto<UserDto>> GetAllWithSort(MetronicPagedResultRequestDto input);
    }
}