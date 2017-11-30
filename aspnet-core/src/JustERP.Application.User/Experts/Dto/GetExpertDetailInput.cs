using Abp.Application.Services.Dto;

namespace JustERP.Application.User.Experts.Dto
{
    public class GetExpertDetailInput : EntityDto<long?>
    {
        public long? ExpertAccountId { get; set; }
    }
}
