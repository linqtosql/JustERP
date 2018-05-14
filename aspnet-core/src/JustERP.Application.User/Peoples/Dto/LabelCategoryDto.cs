﻿using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapFrom(typeof(MtLabelCategory))]
    public class LabelCategoryDto
    {
        public string Name { get; set; }
        public LabelDto[] Labeles { get; set; }
    }
}
