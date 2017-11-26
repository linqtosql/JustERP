using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpert))]
    public class ExpertDetailsDto : ExpertDto
    {
        public string Introduction { get; set; }
        public double Score { get; set; }
        public int ServicesCount { get; set; }
        public ExpertWorkSettingDto[] ExpertWorkSettings { get; set; }
        public string[] ExpertPhotos { get; set; }
        public ExpertCommentDto[] ExpertComments { get; set; }
        public string Organization { get; set; }
        public string Phone { get; set; }
        public decimal Price { get; set; }
    }
}
