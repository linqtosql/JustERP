using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace JustERP.Web.Core.Admin.Controllers
{
    public abstract class JustERPControllerBase: AbpController
    {
        protected JustERPControllerBase()
        {
            LocalizationSourceName = JustERPConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}