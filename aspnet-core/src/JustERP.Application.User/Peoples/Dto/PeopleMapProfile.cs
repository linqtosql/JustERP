using System;
using System.Linq;
using AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    public class PeopleMapProfile : Profile
    {
        public PeopleMapProfile()
        {
            CreateMap<MtPeopleActivity, PeopleActivityDto>()
                .ForMember(p => p.TotalSeconds,
                    d => d.MapFrom(p =>
                        p.EndTime.HasValue ? p.TotalSeconds : (DateTime.Now - p.BeginTime).TotalSeconds))
                .ForMember(p => p.Labels,
                    d => d.MapFrom(p => p.PeopleActivityLabels.ToDictionary(key => key.LabelCategoryId, val => val.LabelName)))
                .ForMember(p => p.BeginTime, d => d.MapFrom(p => p.BeginTime.ToString("yyyy.MM.dd HH:mm")))
                .ForMember(p => p.EndTime,
                    d => d.MapFrom(p => !p.EndTime.HasValue ? "??" : p.EndTime.Value.ToString("HH:mm")));
        }
    }
}
