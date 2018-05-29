﻿using System.Threading;
using Abp.Auditing;
using Abp.Zero.EntityFrameworkCore;
using JustERP.Authorization.Roles;
using JustERP.Authorization.Users;
using JustERP.Core.User.Activities;
using JustERP.Core.User.Pepoles;
using JustERP.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace JustERP.EntityFrameworkCore
{
    public class JustERPDbContext : AbpZeroDbContext<Tenant, Role, User, JustERPDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public virtual DbSet<MtPeople> Peoples { get; set; }
        public virtual DbSet<MtPeopleWechatInfo> PeopleWechatInfos { get; set; }
        public virtual DbSet<MtActivity> Activities { get; set; }
        public virtual DbSet<MtPeopleActivity> PeopleActivities { get; set; }
        public virtual DbSet<MtLabel> Labels { get; set; }
        public virtual DbSet<MtLabelCategory> LabelCategories { get; set; }
        public virtual DbSet<MtPeopleActivityLabel> PeopleActivityLabels { get; set; }

        private string _languageName = null;
        public JustERPDbContext(DbContextOptions<JustERPDbContext> options)
            : base(options)
        {
            _languageName = Thread.CurrentThread.CurrentCulture.Name;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<MtPeople>(b =>
            {
                b.HasMany(e => e.PeopleActivities).WithOne(e => e.People).HasForeignKey(e => e.PeopleId).OnDelete(DeleteBehavior.SetNull);
                b.HasMany(e => e.Activities).WithOne(e => e.People).HasForeignKey(e => e.PeopleId).OnDelete(DeleteBehavior.SetNull);

                b.HasAlternateKey(e => e.Openid);
            });

            modelBuilder.Entity<MtActivity>(b =>
            {
                b.HasMany(e => e.PeopleActivities).WithOne(e => e.Activity).HasForeignKey(e => e.ActivityId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<MtActivity>().HasQueryFilter(activity => activity.Language == _languageName);

            modelBuilder.Entity<MtPeopleActivity>(b =>
            {
                b.HasMany(e => e.PeopleActivityLabels).WithOne(e => e.PeopleActivity).HasForeignKey(e => e.PeopleActivityId);

            });

            modelBuilder.Entity<MtLabelCategory>(b =>
            {
                b.HasMany(e => e.Labels).WithOne(e => e.LabelCategory).HasForeignKey(e => e.LabelCategoryId);
            });

            modelBuilder.Entity<MtPeopleWechatInfo>(b =>
            {
                b.HasAlternateKey(e => e.Openid);
            });

            modelBuilder.Entity<AuditLog>(b =>
            {
                b.Property(l => l.BrowserInfo).HasMaxLength(512);
            });
        }
    }
}
