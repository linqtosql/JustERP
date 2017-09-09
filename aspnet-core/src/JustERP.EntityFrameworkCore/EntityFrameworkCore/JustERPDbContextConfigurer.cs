using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace JustERP.EntityFrameworkCore
{
    public static class JustERPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<JustERPDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<JustERPDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}