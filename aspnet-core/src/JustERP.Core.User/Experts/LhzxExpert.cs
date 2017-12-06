using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Charts;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpert : FullAuditedEntity<long>, IExtendableObject
    {
        private const string PhotosKey = "ExpertPhotos";
        public long ExpertAccountId { get; set; }
        public long? ExpertFirstClassId { get; set; }
        public long? ExpertClassId { get; set; }
        public bool IsExpert { get; set; }
        [Required]
        [MaxLength(16)]
        public string Phone { get; set; }

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
        [NotMapped]
        public LhzxExpertPhoto[] ExpertPhotos
        {
            get => this.GetData<LhzxExpertPhoto[]>(PhotosKey);
            set => this.SetData(PhotosKey, value);
        }
        public virtual LhzxExpertAccount ExpertAccount { get; set; }
        public virtual LhzxExpertClass ExpertFirstClass { get; set; }
        public virtual LhzxExpertClass ExpertClass { get; set; }
        public virtual IEnumerable<LhzxExpertWorkSetting> ExpertWorkSettings { get; set; }
        public virtual IEnumerable<LhzxExpertComment> ExpertComments { get; set; }
        public virtual IEnumerable<LhzxExpertComment> CommenterComments { get; set; }
        public virtual IEnumerable<LhzxExpertOrder> ExpertOrders { get; set; }
        public virtual IEnumerable<LhzxExpertOrder> ServerExpertOrders { get; set; }
        public virtual IEnumerable<LhzxExpertOrderChart> SenderExpertCharts { get; set; }
        public virtual IEnumerable<LhzxExpertOrderChart> ReceiverExpertCharts { get; set; }
        public virtual IEnumerable<LhzxExpertFriendShip> ExpertFriendShips { get; set; }

        public string ExtensionData { get; set; }
    }
}
