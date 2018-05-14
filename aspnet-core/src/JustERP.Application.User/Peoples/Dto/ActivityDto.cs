﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapFrom(typeof(MtActivity))]
    public class ActivityDto : EntityDto<long>
    {
        public string Icon { get; set; }
        public string Name { get; set; }
    }
}
