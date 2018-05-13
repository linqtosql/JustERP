using Abp.Dependency;
using JustERP.Core.User.Pepoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace JustERP.Core.User.Authorization
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<MtPeople>, ITransientDependency
    {
        public UserClaimsPrincipalFactory(PeopleManager userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }
    }
}
