using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertFriendShip), typeof(LhzxExpertAnonymousShip))]
    public class ExpertFriendDto : EntityDto<long>
    {
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public int OrderCount { get; set; }
        public bool Anonymous { get; set; }
    }
}
