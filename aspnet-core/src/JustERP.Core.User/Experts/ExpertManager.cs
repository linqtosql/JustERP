using System;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JustERP.Core.User.Experts
{
    public class ExpertManager : UserManager<LhzxExpert>, IDomainService
    {
        private IRepository<LhzxExpert, long> ExpertRepository;


        public LhzxExpert FindByPhone(string name)
        {
            return ExpertRepository.FirstOrDefault(e => e.Phone == name);
        }

        public ExpertManager(
            IRepository<LhzxExpert, long> expertRepository,
            UserStore store,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<LhzxExpert>> logger) : base(
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
            ExpertRepository = expertRepository;
        }
    }
}
