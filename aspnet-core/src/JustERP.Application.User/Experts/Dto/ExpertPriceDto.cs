using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapFrom(typeof(LhzxExpert))]
    public class ExpertPriceDto
    {
        public decimal Price { get; set; }
    }
}
