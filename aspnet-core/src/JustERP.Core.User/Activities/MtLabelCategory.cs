﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace JustERP.Core.User.Activities
{
    public class MtLabelCategory : Entity<long>, IExtendableObject
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        public virtual IEnumerable<MtLabel> Labels { get; set; }
        public string ExtensionData { get; set; }

        public void SetPeopleName(long peopleId, string name)
        {
            this.SetData($"name_{peopleId}", name);
        }

        public string GetPeopleName(long peopleId)
        {
            return this.GetData<string>($"name_{peopleId}");
        }
    }
}
