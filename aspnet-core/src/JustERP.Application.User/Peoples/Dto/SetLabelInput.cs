using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapTo(typeof(MtPeopleActivityLabel))]
    public class SetLabelInput
    {
        public long LabelCategoryId { get; set; }
        public string LabelName { get; set; }
    }
}
