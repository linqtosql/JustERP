using System;
using System.Linq;
using Abp.Timing;
using AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    public class ExpertMapProfile : Profile
    {
        public ExpertMapProfile()
        {
            CreateMap<LhzxExpert, ExpertDto>()
                .ForMember(e => e.ExpertFirstClassName, opt => opt.MapFrom(e => e.ExpertFirstClass.Name))
                .ForMember(e => e.ExpertClassName, opt => opt.MapFrom(e => e.ExpertClass.Name))
                .ForMember(e => e.OnlineStatus, opt => opt.MapFrom(e => e.GetOnlineStatus()));

            CreateMap<LhzxExpert, ExpertDetailsDto>()
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => p.ImageUrl).ToArray()))
                .ForMember(e => e.OnlineStatus, opt => opt.MapFrom(e => e.GetOnlineStatus()));
            CreateMap<LhzxExpert, LoggedInExpertOutput>()
                .ForMember(e => e.OnlineStatus, opt => opt.MapFrom(e => e.GetOnlineStatus()));

            CreateMap<LhzxExpertWorkSetting, ExpertWorkSettingDto>()
                .ForMember(e => e.StartTime, opt => opt.MapFrom(e => e.StartTime))
                .ForMember(e => e.EndTime, opt => opt.MapFrom(e => e.EndTime));

            CreateMap<CreateExpertInput, LhzxExpert>()
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => new LhzxExpertPhoto { ImageUrl = p }).ToArray()));
            CreateMap<LhzxExpert, CreateExpertInput>()
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => p.ImageUrl)));

            CreateMap<LhzxExpertFriendShip, ExpertFriendDto>()
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.ExpertFriend.Name))
                .ForMember(e => e.Avatar, opt => opt.MapFrom(e => e.ExpertFriend.Avatar))
                .ForMember(e => e.Post, opt => opt.MapFrom(e => e.ExpertFriend.Post))
                .ForMember(e => e.Phone, opt => opt.MapFrom(e => e.ExpertFriend.ExpertAccount.UserName))
                .ForMember(e => e.OrderCount, opt => opt.MapFrom(e => 0))
                .ForMember(e => e.Anonymous, opt => opt.MapFrom(e => false));
            CreateMap<LhzxExpertAnonymousShip, ExpertFriendDto>()
                .ForMember(e => e.Phone, opt => opt.MapFrom(e => e.UserName))
                .ForMember(e => e.Anonymous, opt => opt.MapFrom(e => true));

            CreateMap<LhzxExpertComment, ExpertCommentDto>()
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.CommenterExpert.Name))
                .ForMember(e => e.Avatar, opt => opt.MapFrom(e => e.CommenterExpert.Avatar))
                .ForMember(e => e.Organization, opt => opt.MapFrom(e => e.CommenterExpert.Organization))
                .ForMember(e => e.Post, opt => opt.MapFrom(e => e.CommenterExpert.Post));

            CreateMap<CreateExpertWorkSettingInput, LhzxExpertWorkSetting>()
                .ForMember(e => e.StartTime, opt => opt.MapFrom(e => DateTime.Parse(e.StartTime)))
                .ForMember(e => e.EndTime, opt => opt.MapFrom(e => DateTime.Parse(e.EndTime)));
        }
    }
}
