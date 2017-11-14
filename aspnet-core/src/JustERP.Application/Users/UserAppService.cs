using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using JustERP.Authorization;
using JustERP.Authorization.Users;
using JustERP.Users.Dto;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Session;
using JustERP.Authorization.Roles;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.Roles.Dto;

namespace JustERP.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : BaseMetronicTableAppService<User, UserDto, long, GetUsersRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Role> _roleRepository;
        private IRepository<UserOrganizationUnit, long> _userOrganizationUnit;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            IPasswordHasher<User> passwordHasher,
            IRepository<Role> roleRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnit,
            RoleManager roleManager)
            : base(repository)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnit = userOrganizationUnit;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = _passwordHasher.HashPassword(user, input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await _userManager.CreateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task<MetronicPagedResultDto<UserOUnitDto>> GetUsersInOUnit(GetUsersRequestDto input)
        {
            var userOrg = await _organizationUnitRepository.GetAsync(input.OrganizationUnitId);
            var userOrgs = await _userManager.GetUsersInOrganizationUnit(userOrg);
            input.Total = userOrgs.Count;
            var data = ObjectMapper.Map<List<UserOUnitDto>>(userOrgs);
            data.ForEach(d =>
            {
                d.OrganizationUnitId = input.OrganizationUnitId;
            });
            return new MetronicPagedResultDto<UserOUnitDto>
            {
                Data = data,
                Meta = input
            };
        }

        public async Task AddToOUnit(UserOUnitDto[] input)
        {
            foreach (var userOUnitDto in input)
            {
                await _userManager.AddToOrganizationUnitAsync(userOUnitDto.Id, userOUnitDto.OrganizationUnitId);
            }
        }

        public async Task RemoveFromOUnit(UserOUnitDto[] input)
        {
            foreach (var userOUnitDto in input)
            {
                await _userManager.RemoveFromOrganizationUnitAsync(userOUnitDto.Id,
                    userOUnitDto.OrganizationUnitId);
            }
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }


        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(GetUsersRequestDto input)
        {
            var query = Repository.GetAllIncluding(x => x.Roles);
            if (!string.IsNullOrWhiteSpace(input.Search))
            {
                query = query.Where(u =>
                    u.UserName.Contains(input.Search) ||
                    u.FullName.Contains(input.Search) ||
                    u.EmailAddress.Contains(input.Search));
            }
            return query;
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            return await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, GetUsersRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}