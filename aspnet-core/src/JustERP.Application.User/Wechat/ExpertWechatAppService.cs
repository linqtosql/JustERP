using System.Net.Http;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using JustERP.Application.User.Wechat.Dto;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Wechat;
using Newtonsoft.Json;

namespace JustERP.Application.User.Wechat
{
    public class ExpertWechatAppService : IExpertWechatAppService
    {
        private const string AppId = "wxd1e9929bab5029ce";
        private const string AppSecret = "644f585ce47f569406447cef3ebb04cf";
        private static readonly HttpClient HttpClient;
        private ExpertManager _expertManager;
        private IRepository<LhzxExpert, long> _expertRepository;

        public IObjectMapper ObjectMapper { get; set; }
        public IAbpSession AbpSession { get; set; }

        public ExpertWechatAppService(ExpertManager expertManager, IRepository<LhzxExpert, long> expertRepository)
        {
            _expertManager = expertManager;
            _expertRepository = expertRepository;
        }
        static ExpertWechatAppService()
        {
            HttpClient = new HttpClient();
        }

        public string GetAuthenticateUrl(string returnUrl)
        {
            return
                $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={AppId}&redirect_uri={returnUrl}&response_type=code&scope=snsapi_userinfo#wechat_redirect";
        }

        public async Task<TokenInfotDto> GetToken(string code)
        {
            var result = await HttpClient.GetStringAsync(
                $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={AppId}&secret={AppSecret}&code={code}&grant_type=authorization_code");
            var tokenInfo = JsonConvert.DeserializeObject<TokenInfotDto>(result);
            return tokenInfo;
        }

        public async Task<TokenInfotDto> RefreshToken(string refreshToken)
        {
            var result = await HttpClient.GetStringAsync(
                $"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={AppId}&grant_type=refresh_token&refresh_token={refreshToken}");
            var tokenInfo = JsonConvert.DeserializeObject<TokenInfotDto>(result);
            return tokenInfo;
        }

        public async Task<UserInfoDto> GetUserInfo(string accessToken, string openId)
        {
            var result = await HttpClient.GetStringAsync(
                $"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken}&openid={openId}&lang=zh_CN");
            var userInfo = JsonConvert.DeserializeObject<UserInfoDto>(result);
            
            var wechatInfo = ObjectMapper.Map<LhzxExpertWechatInfo>(userInfo);

            wechatInfo = await _expertManager.CreateWechatInfo(wechatInfo);
            if (AbpSession.UserId.HasValue)
            {
                var expert = await _expertRepository.GetAsync(AbpSession.UserId.Value);
                await _expertManager.UpdateExpertFromWechatInfo(expert, wechatInfo);
            }
            return userInfo;
        }
    }
}
