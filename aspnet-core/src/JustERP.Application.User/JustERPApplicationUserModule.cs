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
    public class JustERPApplicationUserModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<JustERPAuthorizationProvider>();
        }

        public override void Initialize()
        {
            Assembly thisAssembly = typeof(JustERPApplicationUserModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}