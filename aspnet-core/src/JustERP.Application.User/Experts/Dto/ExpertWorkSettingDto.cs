using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertWorkSetting))]
    public class ExpertWorkSettingDto
    {
        public int Week { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
