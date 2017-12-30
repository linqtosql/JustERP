using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using JustERP.Application.User.Orders;
using JustERP.Application.User.Wechat;
using JustERP.Application.User.Wechat.Dto;
using JustERP.Web.Core.User.QCloud.Api;
using JustERP.Web.Core.User.QCloud.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.TenPayLibV3;

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
        private IExpertOrderAppService _orderAppService;
        private IHostingEnvironment _hostingEnvironment;

        public WechatController(IExpertWechatAppService wechatAppService,
            IExpertOrderAppService orderAppService,
            IHostingEnvironment environment)
        {
            _wechatAppService = wechatAppService;
            _hostingEnvironment = environment;
            _orderAppService = orderAppService;
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

        [HttpPost]
        public async Task<IActionResult> ScanNotify()
        {
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> PayNotify()
        {
            try
            {
                ResponseHandler resHandler = new ResponseHandler(HttpContext);

                PayNotifyInfoDto notifyInfo;
                //验证请求是否从微信发过来（安全）
                if (_wechatAppService.CheckNotify(resHandler, out notifyInfo))
                {
                    //直到这里，才能认为交易真正成功了，可以进行数据库操作，但是别忘了返回规定格式的消息！
                    await _orderAppService.PayOrder(notifyInfo.out_trade_no);
                }
                else
                {
                    //错误的订单处理

                }

                /* 这里可以进行订单处理的逻辑 */

                //发送支付成功的模板消息
                //try
                //{
                //    string appId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
                //    string openId = resHandler.GetParameter("openid");
                //    var templateData = new WeixinTemplate_PaySuccess("https://weixin.senparc.com", "购买商品", "状态：" + return_code);

                //    Senparc.Weixin.WeixinTrace.SendCustomLog("支付成功模板消息参数", appId + " , " + openId);

                //    var result = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(appId, openId, templateData);
                //}
                //catch (Exception ex)
                //{
                //    Senparc.Weixin.WeixinTrace.SendCustomLog("支付成功模板消息", ex.ToString());
                //}


                string xml = $@"<xml>
                                <return_code><![CDATA[{notifyInfo.return_code}]]></return_code>
                                <return_msg><![CDATA[{notifyInfo.return_msg}]]></return_msg>
                                </xml>";

                return Content(xml, "text/xml");
            }
            catch (Exception ex)
            {
                throw new WeixinException(ex.Message, ex);
            }
        }
    }
}
