using Abp.Domain.Entities;

namespace JustERP.Core.User.Pepoles
{
    public class MtPeopleWechatInfo : Entity<long>
    {
        public long PeopleId { get; set; }
        public virtual MtPeople People { get; set; }
    }
}
