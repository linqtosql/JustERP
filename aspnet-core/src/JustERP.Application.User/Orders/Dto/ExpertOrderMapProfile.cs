using AutoMapper;
using JustERP.Core.User.Orders;
using JustERP.Core.User.Payments;

namespace JustERP.Application.User.Orders.Dto
{
    public class ExpertOrderMapProfile : Profile
    {
        public ExpertOrderMapProfile()
        {
            CreateMap<LhzxExpertOrder, ExpertOrderDto>()
                .ForMember(e => e.ExpertName, opt => opt.MapFrom(e => e.Expert.Name))
                .ForMember(e => e.ServerExpertName, opt => opt.MapFrom(e => e.ServerExpert.Name));

            CreateMap<CreatePaymentResultInput, LhzxExpertOrderPayment>()
                .ForMember(e => e.PaymentNo, opt => opt.MapFrom(e => e.PaymentNo))
                .ForMember(e => e.PaymentTime, opt => opt.MapFrom(e => e.PaymentTime));
        }
    }
}
