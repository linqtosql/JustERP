using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using JustERP.Core.User.Authorization.Event;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Authorization
{
    public class UserLogInManager : ITransientDependency
    {
        private UserClaimsPrincipalFactory _claimsPrincipalFactory;
        private PeopleManager _peopleManager;
        private IRepository<MtPeople, long> _expertRepository;
        private IRepository<MtPeopleWechatInfo, long> _wechatInfoRepository;
        private IUnitOfWorkManager UnitOfWorkManager { get; }

        public IEventBus EventBus { get; set; }
        public UserLogInManager(PeopleManager peopleManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<MtPeople, long> expertRepository,
            IRepository<MtPeopleWechatInfo, long> wechatInfoRepository)
        {
            _peopleManager = peopleManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _expertRepository = expertRepository;
            _wechatInfoRepository = wechatInfoRepository;
            UnitOfWorkManager = unitOfWorkManager;
        }

        public async Task<UserLoginResult> LoginAsync(string openId)
        {
            var user = await _peopleManager.FindByOpenId(openId);

            if (user == null)
            {
                return await Task.FromResult(new UserLoginResult(AbpLoginResultType.InvalidUserNameOrEmailAddress));
            }

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var result = new UserLoginResult(user.Id, principal.Identity as ClaimsIdentity);
            return result;
        }

        public async Task RegisterAsync(string openId)
        {
            var user = await _peopleManager.FindByOpenId(openId);
            if (user != null)
            {
                return;
            }
            var expertAccount = new MtPeople { Openid = openId };
            await _peopleManager.CreateAsync(expertAccount);
            
            UnitOfWorkManager.Current.SaveChanges();

            await EventBus.TriggerAsync(new RegisterCompleteEventData { Register = expertAccount });
        }
    }
}
