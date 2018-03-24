using Abp.Application.Navigation;
using Abp.Localization;
using JustERP.Authorization;

namespace JustERP
{
    public class SystemNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition("Dashboard", new LocalizableString("Dashboard", "JustERP"), url: "/app/home", requiredPermissionName: PermissionNames.Pages))
                .AddItem(new MenuItemDefinition("Tenants", new LocalizableString("Tenants", "JustERP"), url: "/app/tenants", requiredPermissionName: PermissionNames.Pages_Tenants))
                .AddItem(new MenuItemDefinition("Users", new LocalizableString("Users", "JustERP"), url: "/app/users", requiredPermissionName: PermissionNames.Pages_Users))
                .AddItem(new MenuItemDefinition("Roles", new LocalizableString("Roles", "JustERP"), url: "/app/roles", requiredPermissionName: PermissionNames.Pages_Roles))
                .AddItem(new MenuItemDefinition("OrganizationUnits", new LocalizableString("OrganizationUnits", "JustERP"), url: "/app/organizationunits", requiredPermissionName: PermissionNames.Pages_OrganizationUnits))
                .AddItem(new MenuItemDefinition("AuditLogs", new LocalizableString("AuditLogs", "JustERP"), url: "/app/auditlogs", requiredPermissionName: PermissionNames.Pages_AuditLogs))
                .AddItem(new MenuItemDefinition("Languages", new LocalizableString("Languages", "JustERP"), url: "/app/languages", requiredPermissionName: PermissionNames.Pages_Languages));
        }
    }
}
