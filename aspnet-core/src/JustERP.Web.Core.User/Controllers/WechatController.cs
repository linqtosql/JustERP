using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using JustERP.Application.User.Wechat;
using JustERP.Web.Core.User.QCloud.Api;
using JustERP.Web.Core.User.QCloud.Dto;
using JustERP.Web.Core.User.QCloud.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Senparc.Weixin.MP;

namespace JustERP.Web.Core.User.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WechatController : JustERPControllerBase
    {
        private const string Token = "lianhezixun";
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
        public async Task<IActionResult> UploadCos(string accessToken, string mediaId)
        {
            string stUrl = $"http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={accessToken}&media_id={mediaId}";
            var basePath = Path.Combine(_hostingEnvironment.WebRootPath, "temp");
            var fileName = $"{Guid.NewGuid().ToString()}.png";
            var filePath = $"{basePath}\\{fileName}";

            using (WebClient webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(new Uri(stUrl), filePath);
            }

            var uploadParamDic = new Dictionary<string, string>
            {
                {CosParameters.PARA_BIZ_ATTR, string.Empty},
                {CosParameters.PARA_INSERT_ONLY, "0"}
            };

            var cos = new CosCloud(1253333391, "AKIDFQTPEwb6VyUvGSwREtdLxeDeyAYsD84t", "qZ6Xq150nSzQjvzvlS1SlvxumV3UpEXg");
            var uploadResult = cos.UploadFile("yuelinshe", $"/vizcaya/{fileName}", filePath, uploadParamDic);
            var file = JsonConvert.DeserializeObject<UploadResultDto>(uploadResult);
            return Json(file);
        }
    }
}
