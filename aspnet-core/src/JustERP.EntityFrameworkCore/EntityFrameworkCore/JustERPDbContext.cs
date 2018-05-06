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

        public JustERPDbContext(DbContextOptions<JustERPDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MtPeople>(b =>
            {
                b.HasMany(e => e.PeopleActivities).WithOne().HasForeignKey(e => e.PeopleId);
                b.HasMany(e => e.Activities).WithOne().HasForeignKey(e => e.PeopleId);
            });

            modelBuilder.Entity<MtPeopleActivity>(b =>
            {
                b.HasMany(e => e.PeopleActivityLabels).WithOne().HasForeignKey(e => e.PeopleActivityId);
            });

            modelBuilder.Entity<MtLabelCategory>(b =>
            {
                b.HasMany(e => e.Labels).WithOne().HasForeignKey(e => e.LabelCategoryId);
            });
        }
    }
}
