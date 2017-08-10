using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JustERP.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JustERP.Web.Host.Startup
{
    [DependsOn(
       typeof(JustERPWebCoreModule))]
    public class JustERPWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public JustERPWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPWebHostModule).GetAssembly());
        }
    }
}
