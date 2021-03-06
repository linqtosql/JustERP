﻿using Abp;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using JustERP.EntityFrameworkCore.Seed;

namespace JustERP.EntityFrameworkCore
{
    [DependsOn(
        typeof(JustERPCoreModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class JustERPEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<JustERPDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        JustERPDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        JustERPDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
            //auto entity history
            //Configuration.EntityHistory.Selectors.Add(new NamedTypeSelector("EntityHistory", type => true));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(JustERPEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}