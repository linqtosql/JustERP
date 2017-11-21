using System.Threading.Tasks;
using Abp.Application.Services;
using JustERP.Application.User.Accounts.Dto;
using JustERP.Core.User;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Accounts
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        private ExpertManager _expertManager;
        private UserLogInManager _userLogInManager;

        public AccountAppService(ExpertManager expertManager,
            UserLogInManager userLogInManager)
        {
            _expertManager = expertManager;
            _userLogInManager = userLogInManager;
        }

        public async Task<UserLoginResult> Register(RegisterInput input)
        {
            var existsExpert = await _expertManager.FindByUserName(input.UserName);

            if (existsExpert != null)
            {
                return await _userLogInManager.LoginAsync(input.UserName, input.PhoneCode);
            }

            var expert = ObjectMapper.Map<LhzxExpertAccount>(input);

            await _expertManager.CreateAsync(expert);
            UnitOfWorkManager.Current.SaveChanges();

            return await _userLogInManager.LoginAsync(input.UserName, input.PhoneCode);
        }
    }
}
