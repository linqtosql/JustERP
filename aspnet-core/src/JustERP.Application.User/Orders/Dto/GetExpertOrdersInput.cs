using Abp.Application.Services.Dto;

namespace JustERP.Application.User.Orders.Dto
{
    public class GetExpertOrdersInput : PagedResultRequestDto
    {
        public long ExpertId { get; set; }
        public long ServerExpertId { get; set; }
    }
}
