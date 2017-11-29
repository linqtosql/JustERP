using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JustERP.Core.User.Experts
{
    public class ExpertManager : UserManager<LhzxExpertAccount>, IDomainService
    {
        private IRepository<LhzxExpertAccount, long> _accountRepository;
        private IRepository<LhzxExpert, long> _expertRepository;
        private IRepository<LhzxExpertWorkSetting, long> _workSettingRepository;

        public ExpertManager(
            IRepository<LhzxExpertAccount, long> accountRepository,
            IRepository<LhzxExpert, long> expertRepository,
            IRepository<LhzxExpertWorkSetting, long> workSettingRepository,
            UserStore store,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<LhzxExpertAccount>> logger) : base(
            store,
            null,
            null,
            null,
            null,
            null,
            errors,
            services,
            logger)
        {
            _accountRepository = accountRepository;
            _expertRepository = expertRepository;
            _workSettingRepository = workSettingRepository;
        }



        public async Task<LhzxExpertAccount> FindByUserName(string userName)
        {
            return await _accountRepository.FirstOrDefaultAsync(e => e.UserName == userName);
        }

        public override async Task<IdentityResult> CreateAsync(LhzxExpertAccount user)
        {
            Check.NotNull(user, nameof(user));

            user.CreationTime = DateTime.Now;
            user.IsDeleted = false;

            await _accountRepository.InsertAsync(user);

            var expert = new LhzxExpert
            {
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Phone = user.UserName,
                ExpertAccountId = user.Id,
                Name = user.UserName
            };
            await _expertRepository.InsertAsync(expert);

            return IdentityResult.Success;
        }
    }
}
