using System;
using Abp.AutoMapper;
using JustERP.Core.User.Orders;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrder))]
    public class ExpertOrderDetailsDto : ExpertOrderDto
    {
        public int LastConfirmDatetime => (int)(CreationTime.AddMinutes(5) - DateTime.Now).TotalSeconds;

        public DateTime CreationTime { get; set; }
        public int TotalDuration { get; set; }
    }
}
