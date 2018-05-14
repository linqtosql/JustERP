using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapFrom(typeof(MtPeopleActivityLabel))]
    public class ActivityLabelDto : EntityDto<long>
    {
        public string LabelName { get; set; }
    }
}
