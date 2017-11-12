using System.Collections.Generic;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace JustERP.Authorization
{
    public class JustERPAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var rootPermission = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            rootPermission.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));
            rootPermission.CreateChildPermission(PermissionNames.Pages_Roles, L("Roles"));
            rootPermission.CreateChildPermission(PermissionNames.Pages_AuditLogs, L("AuditLogs"));
            rootPermission.CreateChildPermission(PermissionNames.Pages_OrganizationUnits, L("OrganizationUnits"));
            rootPermission.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, JustERPConsts.LocalizationSourceName);
        }
    }
}
