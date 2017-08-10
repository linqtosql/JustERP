using System.Threading.Tasks;
using Abp.Application.Services;
using JustERP.Authorization.Accounts.Dto;

namespace JustERP.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
