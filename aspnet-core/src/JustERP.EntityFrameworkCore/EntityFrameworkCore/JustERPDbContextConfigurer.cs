using Microsoft.EntityFrameworkCore;

namespace JustERP.EntityFrameworkCore
{
    public static class JustERPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<JustERPDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }
    }
}