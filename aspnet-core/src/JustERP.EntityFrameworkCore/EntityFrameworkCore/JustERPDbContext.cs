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

        public virtual DbSet<MtActivity> Activities { get; set; }

        public JustERPDbContext(DbContextOptions<JustERPDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MtPeople>(b =>
            {
                b.HasMany(e => e.PeopleActivities).WithOne(e => e.People).HasForeignKey(e => e.PeopleId);
                b.HasMany(e => e.Activities).WithOne(e => e.People).HasForeignKey(e => e.PeopleId);
                b.HasOne(e => e.PeopleWechatInfo);
            });
        }
    }
}
