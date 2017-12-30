using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using JustERP.Application.User.Wechat;
using JustERP.Web.Core.User.QCloud.Api;
using JustERP.Web.Core.User.QCloud.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace JustERP.Web.Core.User.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WechatController : JustERPControllerBase
    {
        private const string Token = "lianhezixun";
        private const int CosAppId = 1253333391;
        private const string CosAppSecretId = "AKIDFQTPEwb6VyUvGSwREtdLxeDeyAYsD84t";
        private const string CosAppSecretKey = "qZ6Xq150nSzQjvzvlS1SlvxumV3UpEXg";
        private const string CosBucketName = "yuelinshe";
        private const string CosDirectory = "/vizcaya";
        private IExpertWechatAppService _wechatAppService;
        private IHostingEnvironment _hostingEnvironment;

        public WechatController(IExpertWechatAppService wechatAppService, IHostingEnvironment environment)
        {
            _wechatAppService = wechatAppService;
            _hostingEnvironment = environment;
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
            var redirect = WebUtility.UrlEncode($"https://api.advisors-ally.com/api/wechat/Step2?returnUrl={returnUrl}");

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

            return Redirect($"{WebUtility.UrlDecode(returnUrl)}{(returnUrl.IndexOf("?", StringComparison.Ordinal) > 0 ? "&" : "?")}&openid={userInfo.openid}&token={accessToken}");
        }

        [HttpGet]
        public async Task<IActionResult> JsSdkConfig(string url)
        {
            var config = await _wechatAppService.GetJsSdkConfig(url);
            return Json(config);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCos(string mediaId)
        {
            var basePath = Path.Combine(_hostingEnvironment.WebRootPath, "temp");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            var fileName = $"{Guid.NewGuid().ToString()}.wx";
            var filePath = $"{basePath}\\{fileName}";
            var mediaFile = await _wechatAppService.GetMediaAndSaveAsync(mediaId, filePath);

            var cos = new CosCloud(CosAppId, CosAppSecretId, CosAppSecretKey);
            var uploadResult = cos.UploadFile(CosBucketName, $"{CosDirectory}/{fileName}", mediaFile);
            var file = JsonConvert.DeserializeObject<UploadResultDto>(uploadResult);
            return Json(file);
        }
    }
}
