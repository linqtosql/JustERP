using System.Threading.Tasks;
using Abp.Application.Services;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;

namespace JustERP.Application.User.Wechat
{
    public class ExpertWechatAppService : ApplicationService, IExpertWechatAppService
    {
        public async Task<OAuthAccessTokenResult> GetToken(string code)
        {
            return await OAuthApi.GetAccessTokenAsync(WechatConfig.AppId, WechatConfig.AppSecret, code);
        }
    }
}
