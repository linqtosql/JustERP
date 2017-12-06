using System;
using Abp.AutoMapper;
using JustERP.Core.User.Charts;

namespace JustERP.Application.User.Charts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrderChart))]
    public class ExpertChatDto
    {
        public ExpertDto SenderExpert { get; set; }
        public ExpertDto ReceiverExpert { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
