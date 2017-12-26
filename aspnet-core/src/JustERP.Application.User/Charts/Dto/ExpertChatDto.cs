using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Application.User.Experts.Dto;
using JustERP.Core.User.Charts;

namespace JustERP.Application.User.Charts.Dto
{
    [AutoMapFrom(typeof(LhzxExpertOrderChart))]
    public class ExpertChatDto : EntityDto<long>
    {
        public ExpertBasicDto SenderExpert { get; set; }
        public ExpertBasicDto ReceiverExpert { get; set; }
        public short ChatType { get; set; } = (short)ChatTypes.Text;
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
