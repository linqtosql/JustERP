using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using JustERP.Core.User.Activities;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Authorization
{
    public class UserLogInManager : DomainService
    {
        private UserClaimsPrincipalFactory _claimsPrincipalFactory;
        private PeopleManager _peopleManager;
        private IUnitOfWorkManager UnitOfWorkManager { get; }
        private ActivityManager _activityManager;

        public IEventBus EventBus { get; set; }
        public UserLogInManager(PeopleManager peopleManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            IUnitOfWorkManager unitOfWorkManager,
            ActivityManager activityManager)
        {
            _peopleManager = peopleManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            UnitOfWorkManager = unitOfWorkManager;
            _activityManager = activityManager;
        }

        public async Task<UserLoginResult> LoginAsync(string openId, string avatarUrl = null, string nickName = null)
        {
            var user = await _peopleManager.FindByOpenId(openId);

            if (user == null)
            {
                return await Task.FromResult(new UserLoginResult(AbpLoginResultType.InvalidUserNameOrEmailAddress));
            }

            user.AvatarImg = avatarUrl;
            user.NickName = nickName;

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

            await _activityManager.InitLabels(expertAccount);
            await _activityManager.InitActivities(expertAccount);
            //await EventBus.TriggerAsync(new RegisterCompleteEventData { Register = expertAccount });
        }
    }
}
