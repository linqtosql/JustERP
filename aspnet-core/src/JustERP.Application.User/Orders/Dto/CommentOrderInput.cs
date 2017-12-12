using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapTo(typeof(LhzxExpertComment))]
    public class CommentOrderInput
    {
        public long CommenterExpertId { get; set; }
        public long ExpertOrderId { get; set; }
        public double Score { get; set; } = 10;

        [MaxLength(512)]
        [Required(ErrorMessage = "请输入您对专家的评价")]
        public string Content { get; set; } = "好评";
    }
}
