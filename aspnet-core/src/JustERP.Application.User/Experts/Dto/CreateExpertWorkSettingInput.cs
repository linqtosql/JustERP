﻿using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapTo(typeof(LhzxExpertWorkSetting))]
    public class CreateExpertWorkSettingInput : EntityDto<long>
    {
        public int Week { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}