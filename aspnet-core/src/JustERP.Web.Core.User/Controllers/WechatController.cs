using System;
using System.Net;
using System.Threading.Tasks;
using JustERP.Application.User.Wechat;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP;

namespace JustERP.Web.Core.User.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WechatController : JustERPControllerBase
    {
        private const string Token = "lianhezixun";
        private IExpertWechatAppService _wechatAppService;

        public WechatController(IExpertWechatAppService wechatAppService)
        {
            _wechatAppService = wechatAppService;
        }

        [HttpGet]
        public IActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, Token))
                return Content(echostr);
            return Content("error");
        }

        [HttpGet]
        public IActionResult Step1(string returnUrl)
        {
            var redirect = WebUtility.UrlEncode($"https://api.advisors-ally.com/wechat/Step2?returnUrl={returnUrl}");

            return Redirect(_wechatAppService.GetAuthenticateUrl(redirect));
        }

        [HttpGet]
        public async Task<IActionResult> Step2(string returnUrl, string code, string state)
        {
            var tokenInfo = await _wechatAppService.GetToken(code);
            return RedirectToAction("Step4", new
            {
                returnUrl,
                accessToken = tokenInfo.access_token,
                openId = tokenInfo.openid
            });
        }

        [HttpGet]
        public async Task<IActionResult> Step3(string returnUrl, string refreshToken)
        {
            var tokenInfo = await _wechatAppService.RefreshToken(refreshToken);
            return RedirectToAction("Step4", new
            {
                returnUrl,
                accessToken = tokenInfo.access_token,
                openId = tokenInfo.openid
            });
        }

        [HttpGet]
        public async Task<IActionResult> Step4(string returnUrl, string accessToken, string openId)
        {
            var userInfo = await _wechatAppService.GetUserInfo(accessToken, openId);

            return Redirect($"{WebUtility.UrlDecode(returnUrl)}{(returnUrl.IndexOf("?", StringComparison.Ordinal) > 0 ? "&" : "?")}&openid={userInfo.openid}");
        }

        [HttpGet]
        public async Task<IActionResult> JsSdkConfig()
        {
            var url = Request.GetDisplayUrl();
            var config = await _wechatAppService.GetJsSdkConfig(url);
            return Json(config);
        }
    }
}
