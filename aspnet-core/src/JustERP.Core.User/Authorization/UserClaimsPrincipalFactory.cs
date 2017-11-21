using Abp.Dependency;
using JustERP.Core.User.Experts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace JustERP.Core.User
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<LhzxExpertAccount>, ITransientDependency
    {
        public UserClaimsPrincipalFactory(ExpertManager userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }
    }
}
