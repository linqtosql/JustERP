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


        }
    }
}
