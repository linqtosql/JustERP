using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Authorization.Users;

namespace JustERP.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserOUnitDto : EntityDto<long>
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public long OrganizationUnitId { get; set; }
    }
}
