using System.Collections.Generic;
using Abp.Domain.Entities;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Activities
{
    public class MtLabelCategory : Entity<long>, IExtendableObject
    {
        public string Name { get; set; }
        public virtual IEnumerable<MtLabel> Labels { get; set; }
        public string ExtensionData { get; set; }

        public void SetPeopleName(string value, MtPeople people)
        {
            this.SetData($"name_{people.Id}", value);
        }

        public string GetPeopleName(MtPeople people)
        {
            return this.GetData<string>($"name_{people.Id}");
        }
    }
}
