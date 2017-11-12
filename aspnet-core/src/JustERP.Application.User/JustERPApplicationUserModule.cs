using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace JustERP
{
    [DependsOn(
        typeof(JustERPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class JustERPApplicationUserModule : AbpModule
    {
        public override void PreInitialize()
        {
            
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