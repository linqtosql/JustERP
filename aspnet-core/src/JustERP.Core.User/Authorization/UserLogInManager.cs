using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.UI;
using JustERP.Core.User.Experts;

namespace JustERP.Core.User
{
    public class UserLogInManager : ITransientDependency
    {
        private UserClaimsPrincipalFactory _claimsPrincipalFactory;
        private ExpertManager _expertManager;
        private IUnitOfWorkManager UnitOfWorkManager { get; }
        public UserLogInManager(ExpertManager expertManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _expertManager = expertManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            UnitOfWorkManager = unitOfWorkManager;
        }

        public async Task<UserLoginResult> LoginAsync(string userName, string phoneCode)
        {
            var user = await _expertManager.FindByUserName(userName);

            if (user == null)
            {
                return await Task.FromResult(new UserLoginResult(AbpLoginResultType.InvalidUserNameOrEmailAddress));
            }

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var result = new UserLoginResult(user.Id, principal.Identity as ClaimsIdentity);
            return result;
        }

        public async Task<UserLoginResult> RegisterAsync(string userName, string phoneCode)
        {
            var user = await _expertManager.FindByUserName(userName);
            if (user != null)
            {
                throw new UserFriendlyException($"用户 {userName} 已存在");
            }
            var expertAccount = new LhzxExpertAccount { UserName = userName };
            await _expertManager.CreateAsync(expertAccount);
            UnitOfWorkManager.Current.SaveChanges();

            return await LoginAsync(userName, phoneCode);
        }
    }
}
