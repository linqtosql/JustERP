using System;
using Abp.AutoMapper;
using JustERP.Core.User.Orders;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrderLog))]
    public class ExpertOrderLogDto
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
