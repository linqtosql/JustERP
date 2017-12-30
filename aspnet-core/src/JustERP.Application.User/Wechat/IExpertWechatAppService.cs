using System.Threading.Tasks;
using Abp.Dependency;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;

namespace JustERP.Application.User.Wechat
{
    public interface IExpertWechatAppService : ITransientDependency
    {
        string GetAuthenticateUrl(string returnUrl);

        Task<OAuthAccessTokenResult> GetToken(string code);

        Task<OAuthAccessTokenResult> RefreshToken(string refreshToken);

        Task<OAuthUserInfo> GetUserInfo(string accessToken, string openId);

        Task<JsSdkUiPackage> GetJsSdkConfig(string requestUrl);

        Task<string> GetMediaAndSaveAsync(string mediaId, string fileName);
    }
}
