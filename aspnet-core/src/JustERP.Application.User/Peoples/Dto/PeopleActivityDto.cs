using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapFrom(typeof(MtPeopleActivity))]
    public class PeopleActivityDto : EntityDto<long>
    {
        public long ActivityId { get; set; }
        public string ActivityIcon { get; set; }
        public string ActivityName { get; set; }
        public long BeginTime { get; set; }
        public long? EndTime { get; set; }
        public int TotalSeconds { get; set; }

        public Dictionary<long, string> Labels { get; set; }
        public string Remark { get; set; }
    }
}
