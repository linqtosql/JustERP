using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Castle.Core.Logging;
using JustERP.Application.User.Wechat.Dto;
using JustERP.Application.User.Wechat.TemplateMessage;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Wechat;
using Senparc.Weixin;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat
{
    public class ExpertWechatAppService : IExpertWechatAppService
    {
        private ExpertManager _expertManager;
        private IRepository<LhzxExpert, long> _expertRepository;

        public IObjectMapper ObjectMapper { get; set; }
        public IAbpSession AbpSession { get; set; }
        public IClientInfoProvider ClientInfoProvider { get; set; }
        public ILogger Logger { get; set; }

        public ExpertWechatAppService(ExpertManager expertManager,
            IRepository<LhzxExpert, long> expertRepository)
        {
            _expertManager = expertManager;
            _expertRepository = expertRepository;
        }

        public string GetAuthenticateUrl(string returnUrl)
        {
            return $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={WechatConfig.AppId}&redirect_uri={returnUrl}&response_type=code&scope=snsapi_userinfo#wechat_redirect";

            //return OAuthApi.GetAuthorizeUrl(AppId, returnUrl, string.Empty, OAuthScope.snsapi_userinfo);
        }

        public async Task<OAuthAccessTokenResult> GetToken(string code)
        {
            return await OAuthApi.GetAccessTokenAsync(WechatConfig.AppId, WechatConfig.AppSecret, code);
        }

        public async Task<OAuthAccessTokenResult> RefreshToken(string refreshToken)
        {
            return await OAuthApi.RefreshTokenAsync(WechatConfig.AppId, refreshToken);
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
            return JSSDKHelper.GetJsSdkUiPackageAsync(WechatConfig.AppId, WechatConfig.AppSecret, requestUrl);
        }

        public Task<string> GetMediaAndSaveAsync(string mediaId, string fileName)
        {
            return MediaApi.GetAsync(WechatConfig.AppId, mediaId, fileName);
        }

        public async Task<UnifiedOrderDto> Unifiedorder(CreateUnifiedOrderInput input)
        {
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(WechatConfig.AppId, WechatConfig.MerchantId, input.ProductName, input.TradeNo, input.Amount, ClientInfoProvider.ClientIpAddress, WechatConfig.TenpayNotify, TenPayV3Type.JSAPI, input.OpenId, WechatConfig.PaySecret, input.NonceStr);

            var result = await TenPayV3.UnifiedorderAsync(xmlDataInfo);

            if (string.IsNullOrWhiteSpace(result.prepay_id))
                throw new UserFriendlyException(result.return_msg);
            var orderDto = new UnifiedOrderDto(WechatConfig.AppId, input.TimeStamp, input.NonceStr, result.prepay_id);

            orderDto.Sign(WechatConfig.PaySecret);
            return orderDto;
        }

        public Task<OrderQueryResult> QueryOrder(QueryOrderInput input)
        {
            return TenPayV3.OrderQueryAsync(
                new TenPayV3OrderQueryRequestData(WechatConfig.AppId, WechatConfig.MerchantId, null, input.NonceStr, input.TradeNo, WechatConfig.PaySecret));
        }

        public bool CheckNotify(ResponseHandler handler, out PayNotifyInfoDto info)
        {
            info = ObjectMapper.Map<PayNotifyInfoDto>(handler);

            handler.SetKey(WechatConfig.PaySecret);
            //验证请求是否从微信发过来（安全）
            if (handler.IsTenpaySign() && info.return_code.ToUpper() == "SUCCESS")
            {
                info.out_trade_no = handler.GetParameter("out_trade_no");
                info.total_fee = Convert.ToInt32(handler.GetParameter("total_fee"));
                return true;
            }
            throw new UserFriendlyException(info.return_msg);
        }

        public Task<bool> SendNewOrderMessage(SendOrderMessageInput input)
        {
            return SendTemplateMessage(input.OpenId, new NewOrderMessage(input.OrderId, input.ExpertName, input.ExpertPhone));
        }

        public Task<bool> SendOrderConfirmMessage(SendOrderMessageInput input)
        {
            return SendTemplateMessage(input.OpenId, new OrderConfirmedMessage(input.OrderId, input.OrderNo, input.OrderAmount, input.OrderTime));
        }

        public Task<bool> SendPayedSuccessMessage(SendOrderMessageInput input)
        {
            return SendTemplateMessage(input.OpenId, new PayedSuccessMessage(input.OrderId, input.OrderNo, input.OrderAmount, input.OrderTime));
        }

        private Task<bool> SendTemplateMessage(string openId, ITemplateMessageBase message)
        {
            try
            {
                var result = TemplateApi.SendTemplateMessage(WechatConfig.AppId, openId, message);

                var success = result.errcode == ReturnCode.请求成功;
                return Task.FromResult(success);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message);
                Logger.Info(e.StackTrace);
            }
            return Task.FromResult(false);
        }
    }
}
