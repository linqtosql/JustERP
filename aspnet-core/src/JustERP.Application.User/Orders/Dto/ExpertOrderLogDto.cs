using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Orders;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrderLog))]
    public class ExpertOrderLogDto : EntityDto<long>
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
