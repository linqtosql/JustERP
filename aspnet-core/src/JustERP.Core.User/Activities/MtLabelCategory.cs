using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace JustERP.Core.User.Activities
{
    public class MtLabelCategory : Entity<long>, IExtendableObject
    {
        [Required,MaxLength(32)]
        public string Name { get; set; }
        public virtual IEnumerable<MtLabel> Labels { get; set; }
        public string ExtensionData { get; set; }

        public void SetPeopleName(string value, long peopleId)
        {
            this.SetData($"name_{peopleId}", value);
        }

        public string GetPeopleName(long peopleId)
        {
            return this.GetData<string>($"name_{peopleId}");
        }
    }
}
