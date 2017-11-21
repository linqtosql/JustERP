using System.Threading.Tasks;
using JustERP.Application.User.Accounts.Dto;
using JustERP.Core.User;

namespace JustERP.Application.User.Accounts
{
    public interface IAccountAppService
    {
        Task<UserLoginResult> Register(RegisterInput input);
    }
}