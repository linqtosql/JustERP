using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using JustERP.Application.User.Wechat;
using Microsoft.AspNetCore.Mvc;

namespace JustERP.Web.Core.User.Controllers
{
    public partial class WechatController : JustERPControllerBase
    {
        private IExpertWechatAppService _wechatAppService;
        public WechatController(IExpertWechatAppService wechatAppService)
        {
            _wechatAppService = wechatAppService;
        }
        public IActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            return new ContentResult { Content = echostr };
            //var listStr = new string[3]
            //{
            //    "lianhezixun",
            //    timestamp,
            //    nonce
            //};
            //listStr = listStr.OrderBy(s => s).ToArray();

            //var str = string.Join(string.Empty, listStr);

            //var sha1 = System.Security.Cryptography.SHA1.Create();

            //var hash = sha1.ComputeHash(ObjectToByteArray(str));

            //var hashStr = BitConverter.ToString(hash, 0).Replace("-", string.Empty).ToLower();

            //if (hashStr == signature)
            //    return new ContentResult { Content = echostr };
            //throw new Exception($"{signature},{echostr},{nonce},{timestamp}");
        }

        public IActionResult Step1(string returnUrl)
        {
            var redirect = WebUtility.UrlEncode($"https://api.advisors-ally.com/wechat/Step2?returnUrl={returnUrl}");

            return Redirect(_wechatAppService.GetAuthenticateUrl(redirect));
        }

        public async Task<IActionResult> Step2(string returnUrl, string code, string state)
        {
            var tokenInfo = await _wechatAppService.GetToken(code);
            return RedirectToAction("Step4", new
            {
                returnUrl,
                accessToken = tokenInfo.Access_Token,
                openId = tokenInfo.Openid
            });
        }

        public async Task<IActionResult> Step3(string returnUrl, string refreshToken)
        {
            var tokenInfo = await _wechatAppService.RefreshToken(refreshToken);
            return RedirectToAction("Step4", new
            {
                returnUrl,
                accessToken = tokenInfo.Access_Token,
                openId = tokenInfo.Openid
            });
        }

        public async Task<IActionResult> Step4(string returnUrl, string accessToken, string openId)
        {
            var userInfo = await _wechatAppService.GetUserInfo(accessToken, openId);
            //save to database
            return Redirect($"{WebUtility.UrlDecode(returnUrl)}{(returnUrl.IndexOf("?", StringComparison.Ordinal) > 0 ? "&" : "?")}&openid={openId}");
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
