﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Activities
{
    public class MtPeopleActivity : CreationAuditedEntity<long>
    {
        public long? ActivityId { get; set; }
        public long? PeopleId { get; set; }
        [Required, MaxLength(32)]
        public string ActivityName { get; set; }
        [MaxLength(64), Required]
        public string ActivityIcon { get; set; }

        public DateTime BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int TotalSeconds { get; set; }
        public virtual MtPeople People { get; set; }
        public virtual MtActivity Activity { get; set; }
        public virtual IEnumerable<MtPeopleActivityLabel> PeopleActivityLabels { get; set; }

        public int CalcTotalSeconds()
        {
            return !EndTime.HasValue ? 0 : (int)(EndTime.Value - BeginTime).TotalSeconds;
        }
    }
}
