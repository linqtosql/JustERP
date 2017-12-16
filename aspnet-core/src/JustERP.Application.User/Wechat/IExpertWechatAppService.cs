using System.Threading.Tasks;
using Abp.Dependency;
using JustERP.Application.User.Wechat.Dto;

namespace JustERP.Application.User.Wechat
{
    public interface IExpertWechatAppService : ITransientDependency
    {
        string GetAuthenticateUrl(string returnUrl);

        Task<TokenInfotDto> GetToken(string code);

        Task<TokenInfotDto> RefreshToken(string refreshToken);

        Task<UserInfoDto> GetUserInfo(string accessToken, string openId);
    }
}
