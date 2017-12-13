using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Orders;

namespace JustERP.Application.User.Charts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrder))]
    public class ExpertOrderChatDto : EntityDto<long>
    {
        public int Status { get; set; }
        public long ExpertId { get; set; }
        public long ServerExpertId { get; set; }
        public DateTime CreationTime { get; set; }
        public int TotalDuration { get; set; }
        public List<ExpertChatDto> ExpertOrderCharts { get; set; }
    }
}
