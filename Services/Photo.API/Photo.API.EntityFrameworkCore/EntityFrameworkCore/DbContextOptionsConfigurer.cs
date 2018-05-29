using Microsoft.EntityFrameworkCore;

namespace Photo.API.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<APIDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for APIDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
