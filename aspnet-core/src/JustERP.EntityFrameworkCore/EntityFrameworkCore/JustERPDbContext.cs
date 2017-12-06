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
        public virtual DbSet<LhzxExpertAccount> ExpertAccounts { get; set; }
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LhzxExpertAccount>(b =>
            {
                b.HasOne(e => e.Expert).WithOne(e => e.ExpertAccount).HasForeignKey<LhzxExpert>(e => e.ExpertAccountId);

                b.HasAlternateKey(e => e.UserName);
            });

            modelBuilder.Entity<LhzxExpert>(b =>
            {
                b.HasOne(e => e.ExpertClass).WithMany(e => e.Experts).HasForeignKey(e => e.ExpertClassId);
                b.HasOne(e => e.ExpertFirstClass).WithMany(e => e.FirstClassExperts).HasForeignKey(e => e.ExpertFirstClassId);

                b.HasAlternateKey(e => e.ExpertAccountId);
            });

            modelBuilder.Entity<LhzxExpertComment>(b =>
            {
                b.HasOne(e => e.CommenterExpert).WithMany(e => e.CommenterComments).HasForeignKey(e => e.CommenterExpertId);
                b.HasOne(e => e.Expert).WithMany(e => e.ExpertComments).HasForeignKey(e => e.ExpertId);
                b.HasOne(e => e.ExpertOrder).WithMany(e => e.ExpertComments).HasForeignKey(e => e.ExpertOrderId);
                b.HasMany(e => e.ExpertCommentReplies).WithOne().HasForeignKey(e => e.ParentId);
            });

            modelBuilder.Entity<LhzxExpertOrder>(b =>
            {
                b.HasOne(e => e.Expert).WithMany(e => e.ExpertOrders).HasForeignKey(e => e.ExpertId);
                b.HasOne(e => e.ServerExpert).WithMany(e => e.ServerExpertOrders).HasForeignKey(e => e.ServerExpertId);
                b.HasOne(e => e.ExpertOrderPayment).WithOne(e => e.ExpertOrder)
                    .HasForeignKey<LhzxExpertOrderPayment>(e => e.ExpertOrderId);
                b.HasOne(e => e.ExpertOrderRefund).WithOne(e => e.ExpertOrder)
                    .HasForeignKey<LhzxExpertOrderRefund>(e => e.ExpertOrderId);

                b.HasAlternateKey(e => e.OrderNo);
            });

            modelBuilder.Entity<LhzxExpertOrderLog>(b =>
            {
                b.HasOne(e => e.ExpertOrder).WithMany(e => e.ExpertOrderLogs).HasForeignKey(e => e.ExpertOrderId);
            });

            modelBuilder.Entity<LhzxExpertOrderChart>(b =>
            {
                b.HasOne(e => e.ExpertOrder).WithMany(e => e.ExpertOrderCharts).HasForeignKey(e => e.ExpertOrderId);
                b.HasOne(e => e.SenderExpert).WithMany(e => e.SenderExpertCharts).HasForeignKey(e => e.ExpertId);
                b.HasOne(e => e.ReceiverExpert).WithMany(e => e.ReceiverExpertCharts).HasForeignKey(e => e.ExperReceiverId);
            });

            modelBuilder.Entity<LhzxExpertWorkSetting>(b =>
            {
                b.HasOne(e => e.Expert).WithMany(e => e.ExpertWorkSettings).HasForeignKey(e => e.ExpertId);
            });

            modelBuilder.Entity<LhzxExpertClass>(b =>
            {
                b.HasMany(e => e.ChildrenExpertClasses).WithOne().HasForeignKey(e => e.ParentId);
            });

            modelBuilder.Entity<LhzxExpertFriendShip>(b =>
            {
                b.HasOne(e => e.ExpertFriend).WithMany(e => e.ExpertFriendShips).HasForeignKey(e => e.ExpertFriendId);
            });

        }
    }
}
