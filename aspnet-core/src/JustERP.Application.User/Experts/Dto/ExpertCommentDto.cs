using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertComment))]
    public class ExpertCommentDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Post { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; }
        public double Score { get; set; }
        public DateTime CreationTime { get; set; }
        public List<ExpertCommentDto> ExpertCommentReplies { get; set; }
    }
}
