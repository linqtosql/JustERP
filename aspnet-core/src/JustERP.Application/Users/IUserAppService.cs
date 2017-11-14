using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.Roles.Dto;
using JustERP.Users.Dto;

namespace JustERP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, GetUsersRequestDto, CreateUserDto, UserDto>, IMetronicTableAppService<UserDto, GetUsersRequestDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task<MetronicPagedResultDto<UserOUnitDto>> GetUsersInOUnit(GetUsersRequestDto input);

        Task AddToOUnit(UserOUnitDto[] input);

        Task RemoveFromOUnit(UserOUnitDto[] input);

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}