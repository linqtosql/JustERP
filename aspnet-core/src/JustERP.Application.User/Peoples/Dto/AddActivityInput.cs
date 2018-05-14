using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapTo(typeof(MtActivity))]
    public class AddActivityInput
    {
        public long ActivityId { get; set; }
        public string Name { get; set; }
    }
}
