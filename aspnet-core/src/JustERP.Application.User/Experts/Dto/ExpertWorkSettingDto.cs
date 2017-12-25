using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertWorkSetting))]
    public class ExpertWorkSettingDto : EntityDto<long>
    {
        public int Week { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
