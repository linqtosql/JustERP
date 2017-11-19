using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapTo(typeof(LhzxExpertComment))]
    public class CreateExpertCommentInput
    {
        public long ExpertOrderId { get; set; }
        public double Score { get; set; }
        [MaxLength(512)]
        public string Content { get; set; }
    }
}
