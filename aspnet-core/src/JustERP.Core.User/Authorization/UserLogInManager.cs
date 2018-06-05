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

        public async Task<UserLoginResult> LoginAsync(LoginModel loginModel)
        {
            var user = await _peopleManager.FindByOpenId(loginModel.OpenId);

            if (user == null)
            {
                return await Task.FromResult(new UserLoginResult(AbpLoginResultType.InvalidUserNameOrEmailAddress));
            }

            user.AvatarImg = loginModel.AvatarUrl;
            user.NickName = loginModel.NickName;
            user.TimezoneOffset = loginModel.TimezoneOffset;
            user.TimezoneInfo = loginModel.TimezoneInfo;

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var result = new UserLoginResult(user.Id, principal.Identity as ClaimsIdentity);
            return result;
        }

        public async Task RegisterAsync(string openId)
        {
            var user = await _peopleManager.FindByOpenId(openId);
            if (user == null)
            {
                user = new MtPeople { Openid = openId };
                await _peopleManager.CreateAsync(user);

                UnitOfWorkManager.Current.SaveChanges();
            }

            await _activityManager.InitLabels(user);
            await _activityManager.InitActivities(user);
            //await EventBus.TriggerAsync(new RegisterCompleteEventData { Register = expertAccount });
        }
    }
}
