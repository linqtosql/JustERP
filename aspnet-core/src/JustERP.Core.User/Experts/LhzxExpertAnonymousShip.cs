using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertAnonymousShip: CreationAuditedEntity<long>
    {
        public long ExpertId { get; set; }

        [Required]
        [MaxLength(16)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(16)]
        public string Name { get; set; }

        public virtual LhzxExpert Expert { get; set; }
    }
}
