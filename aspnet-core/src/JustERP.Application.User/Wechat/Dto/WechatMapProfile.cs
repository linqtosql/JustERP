using System;
using AutoMapper;
using JustERP.Core.User.Wechat;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Dto
{
    public class WechatMapProfile : Profile
    {
        public WechatMapProfile()
        {
            CreateMap<OAuthUserInfo, LhzxExpertWechatInfo>().ReverseMap();
            CreateMap<ResponseHandler, PayNotifyInfoDto>()
                .ForMember(e => e.appid, opt => opt.MapFrom(e => e.GetParameter("appid")))
                .ForMember(e => e.attach, opt => opt.MapFrom(e => e.GetParameter("attach")))
                .ForMember(e => e.bank_type, opt => opt.MapFrom(e => e.GetParameter("bank_type")))
                .ForMember(e => e.fee_type, opt => opt.MapFrom(e => e.GetParameter("fee_type")))
                .ForMember(e => e.is_subscribe, opt => opt.MapFrom(e => e.GetParameter("is_subscribe")))
                .ForMember(e => e.mch_id, opt => opt.MapFrom(e => e.GetParameter("mch_id")))
                .ForMember(e => e.nonce_str, opt => opt.MapFrom(e => e.GetParameter("nonce_str")))
                .ForMember(e => e.openid, opt => opt.MapFrom(e => e.GetParameter("openid")))
                .ForMember(e => e.out_trade_no, opt => opt.MapFrom(e => e.GetParameter("out_trade_no")))
                .ForMember(e => e.result_code, opt => opt.MapFrom(e => e.GetParameter("result_code")))
                .ForMember(e => e.return_code, opt => opt.MapFrom(e => e.GetParameter("return_code")))
                .ForMember(e => e.return_msg, opt => opt.MapFrom(e => e.GetParameter("return_msg")))
                .ForMember(e => e.sign, opt => opt.MapFrom(e => e.GetParameter("sign")))
                .ForMember(e => e.sub_mch_id, opt => opt.MapFrom(e => e.GetParameter("sub_mch_id")))
                .ForMember(e => e.time_end, opt => opt.MapFrom(e => e.GetParameter("time_end")))
                .ForMember(e => e.total_fee, opt => opt.MapFrom(e => Int32.Parse(e.GetParameter("total_fee"))))
                .ForMember(e => e.trade_type, opt => opt.MapFrom(e => e.GetParameter("trade_type")))
                .ForMember(e => e.transaction_id, opt => opt.MapFrom(e => e.GetParameter("transaction_id")));

            CreateMap<OrderQueryResult, PayNotifyInfoDto>()
                .ForMember(e => e.appid, opt => opt.MapFrom(e => e.appid))
                .ForMember(e => e.attach, opt => opt.MapFrom(e => e.attach))
                .ForMember(e => e.bank_type, opt => opt.MapFrom(e => e.bank_type))
                .ForMember(e => e.fee_type, opt => opt.MapFrom(e => e.fee_type))
                .ForMember(e => e.is_subscribe, opt => opt.MapFrom(e => e.is_subscribe))
                .ForMember(e => e.mch_id, opt => opt.MapFrom(e => e.mch_id))
                .ForMember(e => e.nonce_str, opt => opt.MapFrom(e => e.nonce_str))
                .ForMember(e => e.openid, opt => opt.MapFrom(e => e.openid))
                .ForMember(e => e.out_trade_no, opt => opt.MapFrom(e => e.out_trade_no))
                .ForMember(e => e.result_code, opt => opt.MapFrom(e => e.result_code))
                .ForMember(e => e.return_code, opt => opt.MapFrom(e => e.return_code))
                .ForMember(e => e.return_msg, opt => opt.MapFrom(e => e.return_msg))
                .ForMember(e => e.sign, opt => opt.MapFrom(e => e.sign))
                .ForMember(e => e.sub_mch_id, opt => opt.MapFrom(e => e.sub_mch_id))
                .ForMember(e => e.time_end, opt => opt.MapFrom(e => e.time_end))
                .ForMember(e => e.total_fee, opt => opt.MapFrom(e => Int32.Parse(e.total_fee)))
                .ForMember(e => e.trade_type, opt => opt.MapFrom(e => e.trade_type))
                .ForMember(e => e.transaction_id, opt => opt.MapFrom(e => e.transaction_id));
        }
    }
}
