﻿using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

#if FEATURE_SIGNALR
using Abp.Web.SignalR;
#endif

namespace JustERP
{
    [DependsOn(
         typeof(JustERPApplicationUserModule),
         typeof(JustERPWebCoreModule)
#if FEATURE_SIGNALR 
        ,typeof(AbpWebSignalRModule)
#endif
     )]
    public class JustERPWebCoreUserModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(JustERPApplicationUserModule).GetAssembly()
                 );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPWebCoreUserModule).GetAssembly());
        }
    }
}
