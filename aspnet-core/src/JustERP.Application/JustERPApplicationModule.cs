using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JustERP.Authorization;

namespace JustERP
{
    [DependsOn(
        typeof(JustERPCoreModule),
        typeof(AbpAutoMapperModule))]
    public class JustERPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<JustERPAuthorizationProvider>();
            Configuration.Navigation.Providers.Add<SystemNavigationProvider>();
        }

        public override void Initialize()
        {
            Assembly thisAssembly = typeof(JustERPApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}