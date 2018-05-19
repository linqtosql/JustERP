using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    public class SetLabelInput
    {
        public long PeopleActivityId { get; set; }
        public ActivityLabelInput[] Labels { get; set; }
    }

    [AutoMapTo(typeof(MtPeopleActivityLabel))]
    public class ActivityLabelInput
    {
        public long LabelCategoryId { get; set; }
        public string LabelName { get; set; }
    }
}
