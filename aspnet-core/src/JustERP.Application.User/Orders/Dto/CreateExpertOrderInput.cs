using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Orders;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapTo(typeof(LhzxExpertOrder))]
    public class CreateExpertOrderInput
    {
        public long ServerExpertId { get; set; }
        [Required(ErrorMessage = "请描述您需要咨询的问题")]
        public string QuestionRemark { get; set; }
        [Required(ErrorMessage = "必须选择要咨询的节数")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "必须选择要咨询的节数")]
        public int TotalDuration { get; set; }
    }
}
