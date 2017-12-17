using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Wechat;

namespace JustERP.Core.User
{
    public class UserLogInManager : ITransientDependency
    {
        private UserClaimsPrincipalFactory _claimsPrincipalFactory;
        private ExpertManager _expertManager;
        private IRepository<LhzxExpert, long> _expertRepository;
        private IRepository<LhzxExpertWechatInfo, long> _wechatInfoRepository;
        private IUnitOfWorkManager UnitOfWorkManager { get; }
        public UserLogInManager(ExpertManager expertManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<LhzxExpert, long> expertRepository,
            IRepository<LhzxExpertWechatInfo, long> wechatInfoRepository)
        {
            _expertManager = expertManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _expertRepository = expertRepository;
            _wechatInfoRepository = wechatInfoRepository;
            UnitOfWorkManager = unitOfWorkManager;
        }

        public async Task<UserLoginResult> LoginAsync(string userName, string phoneCode, string openId = null)
        {
            var user = await _expertManager.FindByUserName(userName);

            if (user == null)
            {
                return await Task.FromResult(new UserLoginResult(AbpLoginResultType.InvalidUserNameOrEmailAddress));
            }

            if (!string.IsNullOrWhiteSpace(openId))
            {
                var wechatInfo = await _wechatInfoRepository.FirstOrDefaultAsync(f => f.Openid == openId);
                if (wechatInfo != null)
                {
                    var expert = await _expertRepository.FirstOrDefaultAsync(e => e.ExpertAccountId == user.Id);
                    await _expertManager.UpdateExpertFromWechatInfo(expert, wechatInfo);
                    await _expertRepository.UpdateAsync(expert);
                }
            }

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var result = new UserLoginResult(user.Id, principal.Identity as ClaimsIdentity);
            return result;
        }

        public async Task<UserLoginResult> RegisterAsync(string userName, string phoneCode, string openId = null)
        {
            var user = await _expertManager.FindByUserName(userName);
            if (user != null)
            {
                throw new UserFriendlyException($"用户 {userName} 已存在");
            }
            var expertAccount = new LhzxExpertAccount { UserName = userName };
            await _expertManager.CreateAsync(expertAccount);
            UnitOfWorkManager.Current.SaveChanges();

            return await LoginAsync(userName, phoneCode, openId);
        }
    }
}
