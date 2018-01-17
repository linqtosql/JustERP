using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JustERP.Application.User.Wechat;
using JustERP.Core.User;

namespace JustERP
{
    [DependsOn(
        typeof(JustERPCoreUserModule),
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

            WechatConfig.Init();
        }
    }
}