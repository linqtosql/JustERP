using Abp.Zero.EntityFrameworkCore;
using JustERP.Authorization.Roles;
using JustERP.Authorization.Users;
using JustERP.Core.User.Charts;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Orders;
using JustERP.Core.User.Payments;
using JustERP.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace JustERP.EntityFrameworkCore
{
    public class JustERPDbContext : AbpZeroDbContext<Tenant, Role, User, JustERPDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public virtual DbSet<LhzxExpert> Experts { get; set; }
        public virtual DbSet<LhzxExpertClass> ExpertClasses { get; set; }
        public virtual DbSet<LhzxExpertComment> ExpertComments { get; set; }
        public virtual DbSet<LhzxExpertFriendShip> ExpertFriendShips { get; set; }
        public virtual DbSet<LhzxExpertWorkSetting> ExpertWorkSettings { get; set; }
        public virtual DbSet<LhzxExpertOrderChart> ExpertOrderCharts { get; set; }
        public virtual DbSet<LhzxExpertOrder> ExpertOrders { get; set; }
        public virtual DbSet<LhzxExpertOrderLog> ExpertOrderLogs { get; set; }
        public virtual DbSet<LhzxExpertOrderPayment> ExpertOrderPayments { get; set; }
        public virtual DbSet<LhzxExpertOrderRefund> ExpertOrderRefunds { get; set; }


        public JustERPDbContext(DbContextOptions<JustERPDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LhzxExpert>().HasOne(e => e.ExpertClass).WithMany(c => c.Experts).HasForeignKey(e => e.ExpertClassId);
            modelBuilder.Entity<LhzxExpertComment>().HasOne(e => e.Expert).WithMany(c => c.ExpertComments).HasForeignKey(e => e.ExpertId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
