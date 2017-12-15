using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JustERP.Core.User.Experts
{
    public class ExpertManager : UserManager<LhzxExpertAccount>, IDomainService
    {
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }
        private IRepository<LhzxExpertAccount, long> _accountRepository;
        private IRepository<LhzxExpert, long> _expertRepository;
        private IRepository<LhzxExpertWorkSetting, long> _workSettingRepository;
        private IRepository<LhzxExpertFriendShip, long> _friendShipRepository;
        private IRepository<LhzxExpertAnonymousShip, long> _anonymousRepository;

        public ExpertManager(
            IRepository<LhzxExpertAccount, long> accountRepository,
            IRepository<LhzxExpert, long> expertRepository,
            IRepository<LhzxExpertWorkSetting, long> workSettingRepository,
            IRepository<LhzxExpertFriendShip, long> friendShipRepository,
            IRepository<LhzxExpertAnonymousShip, long> anonymousRepository,
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
            _friendShipRepository = friendShipRepository;
            _anonymousRepository = anonymousRepository;
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

        public async Task<LhzxExpertFriendShip> CreateExpertFriend(LhzxExpert expert, LhzxExpert friendExpert)
        {
            if (expert.Id == friendExpert.Id)
                throw new UserFriendlyException("您不能添加自己");
            if (await _friendShipRepository.GetAll()
                .AnyAsync(f => f.ExpertId == expert.Id && f.ExpertFriendId == friendExpert.Id))
                throw new UserFriendlyException("您已添加过该用户");

            var friendShip = new LhzxExpertFriendShip
            {
                ExpertId = expert.Id,
                ExpertFriendId = friendExpert.Id
            };

            await _friendShipRepository.InsertAsync(friendShip);

            UnitOfWorkManager.Current.SaveChanges();
            return await _friendShipRepository.GetAllIncluding(f => f.ExpertFriend).SingleOrDefaultAsync(f => f.Id == friendShip.Id);
        }

        public async Task<LhzxExpertAnonymousShip> CreateExpertFriend(LhzxExpert expert,
            LhzxExpertAnonymousShip anonymExpert)
        {
            if (await _expertRepository.GetAll().AnyAsync(e => e.Id == expert.Id && e.ExpertAccount.UserName == anonymExpert.UserName))
                throw new UserFriendlyException("您不能添加自己");
            if (await _anonymousRepository.GetAll().AnyAsync(e => e.UserName == anonymExpert.UserName))
                throw new UserFriendlyException("您已添加过该用户");

            var inserted = await _anonymousRepository.InsertAsync(anonymExpert);
            return inserted;
        }
    }
}
