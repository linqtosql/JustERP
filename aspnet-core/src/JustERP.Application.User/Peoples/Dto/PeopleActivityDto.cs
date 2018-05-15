using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapFrom(typeof(MtPeopleActivity))]
    public class PeopleActivityDto : EntityDto<long>
    {
        public string ActivityIcon { get; set; }
        public string ActivityName { get; set; }
        public DateTime BeginTime { get; set; }
        public int TotalSeconds { get; set; }
    }
}
