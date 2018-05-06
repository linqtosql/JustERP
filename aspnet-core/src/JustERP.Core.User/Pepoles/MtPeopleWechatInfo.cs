using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Pepoles
{
    /// <summary>
    /// 用户的微信资料信息
    /// </summary>
    public class MtPeopleWechatInfo : CreationAuditedEntity<long>
    {
        /// <summary>
        /// openid
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Openid { get; set; }
        /// <summary>
        /// nickname
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Nickname { get; set; }
        /// <summary>
        /// sex
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// province
        /// </summary>
        [MaxLength(16)]
        public string Province { get; set; }
        /// <summary>
        /// city
        /// </summary>
        [MaxLength(16)]
        public string City { get; set; }
        /// <summary>
        /// country
        /// </summary>
        [MaxLength(16)]
        public string Country { get; set; }
        /// <summary>
        /// headimgurl
        /// </summary>
        [Required]
        [MaxLength(512)]
        public string Headimgurl { get; set; }
        /// <summary>
        /// unionid
        /// </summary>
        [MaxLength(128)]
        public string Unionid { get; set; }
    }
}
