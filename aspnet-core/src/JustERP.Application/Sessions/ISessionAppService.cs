using System.Threading.Tasks;
using Abp.Application.Services;
using JustERP.Sessions.Dto;

namespace JustERP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
