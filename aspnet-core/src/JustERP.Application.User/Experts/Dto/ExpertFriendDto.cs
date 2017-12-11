using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertFriendShip))]
    public class ExpertFriendDto
    {
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public int OrderCount { get; set; }
    }
}
