using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.Roles.Dto;
using JustERP.Users.Dto;

namespace JustERP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, GetUsersDto, CreateUserDto, UserDto>, IMetronicTableAppService<UserDto, GetUsersDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
        
        Task<MetronicPagedResultDto<UserOUnitDto>> GetUsersInOUnit(GetUsersDto input);

        Task AddToOUnit(CreateUserOUnitDto[] input);

        Task RemoveFromOUnit(CreateUserOUnitDto[] input);

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}