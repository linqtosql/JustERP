using Abp.Application.Services.Dto;

namespace JustERP.Application.User.Experts.Dto
{
    public class LoggedInExpertOutput : EntityDto<long>
    {
        public bool IsExpert { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public int OnlineStatus { get; set; }
    }
}
