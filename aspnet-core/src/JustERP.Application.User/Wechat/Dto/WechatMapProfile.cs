using AutoMapper;
using JustERP.Core.User.Wechat;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;

namespace JustERP.Application.User.Wechat.Dto
{
    public class WechatMapProfile : Profile
    {
        public WechatMapProfile()
        {
            CreateMap<OAuthUserInfo, LhzxExpertWechatInfo>().ReverseMap();
        }
    }
}
