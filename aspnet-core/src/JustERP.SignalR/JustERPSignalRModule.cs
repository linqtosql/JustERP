using System.Reflection;
using Abp;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace JustERP.SignalR
{
    [DependsOn(typeof(AbpKernelModule))]
    public class JustERPSignalRModule : AbpModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
        }

        public override void Initialize()
        {
            Assembly thisAssembly = typeof(JustERPSignalRModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}
