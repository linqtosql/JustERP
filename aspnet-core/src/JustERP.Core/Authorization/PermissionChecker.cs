using Abp.Authorization;
using JustERP.Authorization.Roles;
using JustERP.Authorization.Users;

namespace JustERP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
