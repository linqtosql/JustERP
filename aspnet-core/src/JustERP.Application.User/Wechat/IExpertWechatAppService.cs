using System.Threading.Tasks;
using Abp.Dependency;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;

namespace JustERP.Application.User.Wechat
{
    public interface IExpertWechatAppService : ITransientDependency
    {
        Task<OAuthAccessTokenResult> GetToken(string code);
    }
}
