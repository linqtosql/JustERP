using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JustERP.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JustERP.Web.Core.Admin
{
    [DependsOn(
        typeof(JustERPApplicationModule),
        typeof(JustERPWebCoreModule))]
    public class JustERPWebCoreAdminModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;
        public JustERPWebCoreAdminModule(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(JustERPApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPWebCoreAdminModule).GetAssembly());
        }
    }
}
