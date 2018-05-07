using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace JustERP.Core.User.Activities
{
    public class MtPeopleActivityLabel : Entity<long>
    {
        public long PeopleActivityId { get; set; }
        public long LabelId { get; set; }
        [Required, MaxLength(64)]
        public long LabelName { get; set; }
        public virtual MtLabel Label { get; set; }
        public virtual MtPeopleActivity PeopleActivity { get; set; }
    }
}
