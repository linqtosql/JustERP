using Abp.Domain.Entities;

namespace JustERP.Core.User.Activities
{
    public class MtPeopleActivityLabel : Entity<long>
    {
        public long PeopleActivityId { get; set; }
        public long LabelId { get; set; }
        public long LabelName { get; set; }
    }
}
