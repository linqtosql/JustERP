using System.Collections.Generic;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapFrom(typeof(LhzxExpertComment))]
    public class ExpertCommentDto
    {
        public string Avatar { get; set; }
        public double Score { get; set; }
        public string Content { get; set; }

        public List<ExpertCommentDto> ExpertCommentReplies { get; set; }
    }
}
