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
                .ForMember(e => e.ExpertWorkSettings, opt => opt.MapFrom(e => e.ExpertWorkSettings.Select(s => $"{s.Week} {s.StartTime:HH:mm}-{s.EndTime:HH:mm}")))
                .ForMember(e => e.ExpertPhotos, opt => opt.MapFrom(e => e.ExpertPhotos.Select(p => p.ImageUrl).ToArray()));
        }
    }
}
