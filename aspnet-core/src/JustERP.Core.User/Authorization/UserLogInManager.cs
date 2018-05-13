using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Authorization
{
    public class UserLogInManager : ITransientDependency
    {
        private UserClaimsPrincipalFactory _claimsPrincipalFactory;
        private PeopleManager _expertManager;
        private IRepository<MtPeople, long> _expertRepository;
        private IRepository<MtPeopleWechatInfo, long> _wechatInfoRepository;
        private IUnitOfWorkManager UnitOfWorkManager { get; }
        public UserLogInManager(PeopleManager expertManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<MtPeople, long> expertRepository,
            IRepository<MtPeopleWechatInfo, long> wechatInfoRepository)
        {
            _expertManager = expertManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _expertRepository = expertRepository;
            _wechatInfoRepository = wechatInfoRepository;
            UnitOfWorkManager = unitOfWorkManager;
        }

        public async Task<UserLoginResult> LoginAsync(string openId = null)
        {
            var user = await _expertManager.FindByOpenId(openId);

            if (user == null)
            {
                return await Task.FromResult(new UserLoginResult(AbpLoginResultType.InvalidUserNameOrEmailAddress));
            }

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var result = new UserLoginResult(user.Id, principal.Identity as ClaimsIdentity);
            return result;
        }

        public async Task<UserLoginResult> RegisterAsync(string openId = null)
        {
            var user = await _expertManager.FindByOpenId(openId);
            if (user != null)
            {
                throw new UserFriendlyException("用户已存在");
            }
            var expertAccount = new MtPeople { Openid = openId };
            await _expertManager.CreateAsync(expertAccount);
            UnitOfWorkManager.Current.SaveChanges();

            return await LoginAsync(openId);
        }
    }
}
