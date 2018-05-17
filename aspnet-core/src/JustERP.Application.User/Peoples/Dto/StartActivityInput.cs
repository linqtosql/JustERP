using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapTo(typeof(MtPeopleActivity))]
    public class StartActivityInput
    {
        public long ActivityId { get; set; }
    }
}
