using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Charts;

namespace JustERP.Application.User.Charts.Dto
{
    [AutoMapTo(typeof(LhzxExpertOrderChart))]
    public class CreateExpertChatInput
    {
        public long ExpertOrderId { get; set; }
        public long ExpertId { get; set; }
        public long ExperReceiverId { get; set; }
        public short ChatType { get; set; } = (short)ChatTypes.Text;
        [MaxLength(512, ErrorMessage = "聊天内容最大长度不得超过512个字")]
        [Required(ErrorMessage = "聊天内容不能为空")]
        public string Content { get; set; }
    }
}
