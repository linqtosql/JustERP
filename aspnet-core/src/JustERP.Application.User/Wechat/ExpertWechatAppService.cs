using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using JustERP.Application.User.Wechat.Dto;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Wechat;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat
{
    public class ExpertWechatAppService : IExpertWechatAppService
    {
        private const string AppId = "wxd1e9929bab5029ce";
        private const string AppSecret = "644f585ce47f569406447cef3ebb04cf";
        private const string MerchantId = "1489631162";
        private const string PaySecret = "LianHeZixun586742POITFCijneik845";
        private const string TenpayNotify = "https://api.advisors-ally.com/api/Wechat/PayNotify/";

        private ExpertManager _expertManager;
        private IRepository<LhzxExpert, long> _expertRepository;

        public IObjectMapper ObjectMapper { get; set; }
        public IAbpSession AbpSession { get; set; }
        public IClientInfoProvider ClientInfoProvider { get; set; }

        public ExpertWechatAppService(ExpertManager expertManager,
            IRepository<LhzxExpert, long> expertRepository)
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

        public Task<string> GetMediaAndSaveAsync(string mediaId, string fileName)
        {
            return MediaApi.GetAsync(AppId, mediaId, fileName);
        }

        public async Task<UnifiedOrderDto> Unifiedorder(CreateUnifiedOrderInput input)
        {
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(AppId, MerchantId, input.ProductName, input.TradeNo, input.Amount, ClientInfoProvider.ClientIpAddress, TenpayNotify, TenPayV3Type.JSAPI, input.OpenId, PaySecret, input.NonceStr);

            var result = await TenPayV3.UnifiedorderAsync(xmlDataInfo);

            if (string.IsNullOrWhiteSpace(result.prepay_id))
                throw new UserFriendlyException(result.return_msg);
            var orderDto = new UnifiedOrderDto(AppId, input.TimeStamp, input.NonceStr, result.prepay_id);

            orderDto.Sign(PaySecret);
            return orderDto;
        }

        public Task<OrderQueryResult> QueryOrder(QueryOrderInput input)
        {
            return TenPayV3.OrderQueryAsync(
                new TenPayV3OrderQueryRequestData(AppId, MerchantId, null, input.NonceStr, input.TradeNo, PaySecret));
        }

        public bool CheckNotify(ResponseHandler handler, out PayNotifyInfoDto info)
        {
            info = ObjectMapper.Map<PayNotifyInfoDto>(handler);

            handler.SetKey(PaySecret);
            //验证请求是否从微信发过来（安全）
            if (handler.IsTenpaySign() && info.return_code.ToUpper() == "SUCCESS")
            {
                info.out_trade_no = handler.GetParameter("out_trade_no");
                info.total_fee = Convert.ToInt32(handler.GetParameter("total_fee"));
                return true;
            }
            throw new UserFriendlyException(info.return_msg);
        }


    }
}
