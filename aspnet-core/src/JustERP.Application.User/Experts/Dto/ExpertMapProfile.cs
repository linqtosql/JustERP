using System.Linq;
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
                .ForMember(e => e.ExpertClassName, opt => opt.MapFrom(e => e.ExpertClass.Name));

            CreateMap<LhzxExpert, ExpertDetailsDto>()
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => p.ImageUrl).ToArray()));

            CreateMap<LhzxExpertWorkSetting, ExpertWorkSettingDto>()
                .ForMember(e => e.StartTime, opt => opt.MapFrom(e => e.StartTime.ToString("HH:mm")))
                .ForMember(e => e.EndTime, opt => opt.MapFrom(e => e.EndTime.ToString("HH:mm")));

            CreateMap<CreateExpertInput, LhzxExpert>()
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => new LhzxExpertPhoto { ImageUrl = p })));
            CreateMap<LhzxExpert, CreateExpertInput>()
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => p.ImageUrl)));

            CreateMap<LhzxExpertFriendShip, ExpertFriendDto>()
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.ExpertFriend.Name))
                .ForMember(e => e.Avatar, opt => opt.MapFrom(e => e.ExpertFriend.Avatar))
                .ForMember(e => e.Post, opt => opt.MapFrom(e => e.ExpertFriend.Post))
                .ForMember(e => e.Phone, opt => opt.MapFrom(e => e.ExpertFriend.Phone))
                .ForMember(e => e.OrderCount, opt => opt.MapFrom(e => 0));
        }
    }
}
