using System.Threading.Tasks;
using Abp.Dependency;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

namespace JustERP.Application.User.Wechat
{
    public interface IExpertWechatAppService : ITransientDependency
    {
        Task<JsCode2JsonResult> GetToken(string code);
    }
}
