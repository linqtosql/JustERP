using System.Reflection;
using Abp;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JustERP.SignalR.Hub;
using Microsoft.AspNetCore.SignalR;

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

            //use hublifetimemanager to invoke method out of hub
            IocManager.Register(typeof(HubLifetimeManager<ExpertChatHub>), typeof(DefaultHubLifetimeManager<ExpertChatHub>));

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}
