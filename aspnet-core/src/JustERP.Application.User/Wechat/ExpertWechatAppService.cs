using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Web.Models;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

namespace JustERP.Application.User.Wechat
{
    public class ExpertWechatAppService : ApplicationService, IExpertWechatAppService
    {
        [DontWrapResult]
        public async Task<JsCode2JsonResult> GetToken(string code)
        {
            var jsonResult = await SnsApi.JsCode2JsonAsync(WechatConfig.AppId, WechatConfig.AppSecret, code);
            return jsonResult;
        }
    }
}
