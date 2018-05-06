using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;

namespace JustERP.Core.User
{
    [DependsOn(typeof(JustERPCoreModule),
        typeof(AbpZeroCoreModule))]
    public class JustERPCoreUserModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPCoreUserModule).GetAssembly());
        }
    }
}
