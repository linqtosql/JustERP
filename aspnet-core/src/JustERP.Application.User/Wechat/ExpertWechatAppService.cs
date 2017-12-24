using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Wechat;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;

namespace JustERP.Application.User.Wechat
{
    public class ExpertWechatAppService : IExpertWechatAppService
    {
        private const string AppId = "wxd1e9929bab5029ce";
        private const string AppSecret = "644f585ce47f569406447cef3ebb04cf";
        private ExpertManager _expertManager;
        private IRepository<LhzxExpert, long> _expertRepository;

        public IObjectMapper ObjectMapper { get; set; }
        public IAbpSession AbpSession { get; set; }

        public ExpertWechatAppService(ExpertManager expertManager, IRepository<LhzxExpert, long> expertRepository)
        {
            _expertManager = expertManager;
            _expertRepository = expertRepository;
        }

        public string GetAuthenticateUrl(string returnUrl)
        {
            return $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={AppId}&redirect_uri={returnUrl}&response_type=code&scope=snsapi_userinfo#wechat_redirect";

            //return OAuthApi.GetAuthorizeUrl(AppId, returnUrl, string.Empty, OAuthScope.snsapi_userinfo);
        }

        public async Task<OAuthAccessTokenResult> GetToken(string code)
        {
            return await OAuthApi.GetAccessTokenAsync(AppId, AppSecret, code);
        }

        public async Task<OAuthAccessTokenResult> RefreshToken(string refreshToken)
        {
            return await OAuthApi.RefreshTokenAsync(AppId, refreshToken);
        }

        public async Task<OAuthUserInfo> GetUserInfo(string accessToken, string openId)
        {
            var userInfo = await OAuthApi.GetUserInfoAsync(accessToken, openId);

            var wechatInfo = ObjectMapper.Map<LhzxExpertWechatInfo>(userInfo);

            wechatInfo = await _expertManager.CreateWechatInfo(wechatInfo);
            if (AbpSession.UserId.HasValue)
            {
                var expert = await _expertRepository.GetAsync(AbpSession.UserId.Value);
                await _expertManager.UpdateExpertFromWechatInfo(expert, wechatInfo);
            }
            return userInfo;
        }

        public Task<JsSdkUiPackage> GetJsSdkConfig(string requestUrl)
        {
            return JSSDKHelper.GetJsSdkUiPackageAsync(AppId, AppSecret, requestUrl);
        }
    }
}
