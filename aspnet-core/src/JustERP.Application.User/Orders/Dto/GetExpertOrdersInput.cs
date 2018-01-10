using Abp.Application.Services.Dto;

namespace JustERP.Application.User.Orders.Dto
{
    public class GetExpertOrdersInput : DefaultPagedResultRequestDto
    {
        public long ExpertId { get; set; }
        public long ServerExpertId { get; set; }
    }

    public class DefaultPagedResultRequestDto : PagedResultRequestDto
    {
        protected DefaultPagedResultRequestDto() => MaxResultCount = 100;
    }
}
