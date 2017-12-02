using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Orders;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrder))]
    public class ExpertOrderDto : EntityDto<long>
    {
        public string OrderNo { get; set; }
        public long ExpertId { get; set; }
        public long ServerExpertId { get; set; }
        public string ExpertName { get; set; }
        public string ServerExpertName { get; set; }
        public int Status { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string QuestionRemark { get; set; }
    }
}
