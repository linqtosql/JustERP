using System;
using Abp.AutoMapper;
using JustERP.Application.User.Experts.Dto;
using JustERP.Core.User.Charts;

namespace JustERP.Application.User.Charts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrderChart))]
    public class ExpertChatDto
    {
        public ExpertBasicDto SenderExpert { get; set; }
        public ExpertBasicDto ReceiverExpert { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
