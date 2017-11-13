using Abp.Modules;
using Abp.Reflection.Extensions;
using JustERP.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JustERP.Web.Host.User
{
    [DependsOn(
       typeof(JustERPWebCoreUserModule))]
    public class JustERPWebHostUserModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public JustERPWebHostUserModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPWebHostUserModule).GetAssembly());
        }
    }
}
