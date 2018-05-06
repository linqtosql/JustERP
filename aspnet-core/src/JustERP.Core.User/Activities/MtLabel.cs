using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Activities
{
    public class MtLabel : Entity<long>
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }

        public long LabelCategoryId { get; set; }
        public long? PeopleId { get; set; }

        public virtual MtLabelCategory LabelCategory { get; set; }
        public virtual MtPeople People { get; set; }
    }
}
