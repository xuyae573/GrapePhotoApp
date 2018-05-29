using Notification.API.Configuration;
using Notification.API.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Notification.API.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class APIDbContextFactory : IDesignTimeDbContextFactory<APIDbContext>
    {
        public APIDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<APIDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(APIConsts.ConnectionStringName)
            );

            return new APIDbContext(builder.Options);
        }
    }
}