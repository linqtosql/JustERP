using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Web.Models;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

namespace JustERP.Application.User.Wechat
{
    public interface IExpertWechatAppService : ITransientDependency
    {
        [DontWrapResult]
        Task<JsCode2JsonResult> GetToken(string code);
    }
}
