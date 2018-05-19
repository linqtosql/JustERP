using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace JustERP.Core.User.Activities
{
    public class MtPeopleActivityLabel : Entity<long>
    {
        public long PeopleActivityId { get; set; }
        public long LabelCategoryId { get; set; }
        [Required, MaxLength(64)]
        public string LabelName { get; set; }

        public virtual MtLabelCategory LabelCategory { get; set; }
        public virtual MtPeopleActivity PeopleActivity { get; set; }
    }
}
