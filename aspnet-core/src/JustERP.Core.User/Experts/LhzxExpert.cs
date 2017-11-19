using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpert : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>, IExtendableObject
    {
        public const string PhotosKey = "ExpertPhotos";
        [Required]
        public long ExpertFirstClassId { get; set; }
        [Required]
        public long ExpertClassId { get; set; }
        public int? ExpertType { get; set; }
        [Required]
        [MaxLength(16)]
        public string Phone { get; set; }
        [MaxLength(32)]
        public string Password { get; set; }
        [Required]
        [MaxLength(16)]
        public string Name { get; set; }
        [MaxLength(16)]
        public string NickName { get; set; }
        [MaxLength(128)]
        public string Avatar { get; set; }
        [MaxLength(128)]
        public string BackgroundImage { get; set; }
        [MaxLength(32)]
        public string Organization { get; set; }
        [MaxLength(32)]
        public string Post { get; set; }
        public int? WorkYears { get; set; }
        [MaxLength(64)]
        public string Tags { get; set; }
        [MaxLength(512)]
        public string Introduction { get; set; }
        [MaxLength(512)]
        public string Speciality { get; set; }

        public decimal? Price { get; set; }
        public int? DurationPerTime { get; set; }
        [MaxLength(32)]
        public string AlipayAccount { get; set; }
        [MaxLength(32)]
        public string WeixinAccount { get; set; }
        public double? Score { get; set; }
        public double? AvgTime { get; set; }
        public int? ServicesCount { get; set; }
        public int? OnlineStatus { get; set; }
        [MaxLength(16)]
        public string RegisterIpAddress { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsActive { get; set; }
        public virtual LhzxExpertClass ExpertFirstClass { get; set; }
        public virtual LhzxExpertClass ExpertClass { get; set; }
        public virtual IEnumerable<LhzxExpertWorkSetting> ExpertWorkSettings { get; set; }
        public virtual IEnumerable<LhzxExpertComment> ExpertComments { get; set; }
        public virtual IEnumerable<LhzxExpertComment> CommenterComments { get; set; }
        public virtual IEnumerable<LhzxExpertOrder> ExpertOrders { get; set; }
        public virtual IEnumerable<LhzxExpertOrder> ServerExpertOrders { get; set; }

        [CanBeNull]
        public virtual Authorization.Users.User CreatorUser { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User DeleterUser { get; set; }

        public string ExtensionData { get; set; }
    }
}
