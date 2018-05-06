using Abp.Modules;
using Abp.Reflection.Extensions;

namespace JustERP.Core.User
{
    [DependsOn(typeof(JustERPCoreModule))]
    public class JustERPCoreUserModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPCoreUserModule).GetAssembly());
        }
    }
}
