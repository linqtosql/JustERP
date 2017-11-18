﻿using System.Security.Claims;
using System.Threading.Tasks;
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
        protected IUnitOfWorkManager UnitOfWorkManager { get; }
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
            var user = _expertManager.FindByPhone(userName);

            if (user == null)
            {
                throw new UserFriendlyException("当前用户不存在");
            }

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var result = new UserLoginResult(user.Id, principal.Identity as ClaimsIdentity);
            return result;
        }
    }
}
