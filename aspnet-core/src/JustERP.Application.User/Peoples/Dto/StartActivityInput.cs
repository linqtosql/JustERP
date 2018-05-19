using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapTo(typeof(MtPeopleActivity))]
    public class StartActivityInput
    {
        /// <summary>
        /// 要开始的活动ID
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 要结束的用户活动ID
        /// </summary>
        public long? PeopleActivityId { get; set; }
    }
}
