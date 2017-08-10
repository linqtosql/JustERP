using JustERP.Configuration;
using JustERP.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace JustERP.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class JustERPDbContextFactory : IDbContextFactory<JustERPDbContext>
    {
        public JustERPDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<JustERPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            JustERPDbContextConfigurer.Configure(builder, configuration.GetConnectionString(JustERPConsts.ConnectionStringName));
            
            return new JustERPDbContext(builder.Options);
        }
    }
}