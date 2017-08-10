using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using JustERP.Configuration.Dto;

namespace JustERP.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : JustERPAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
