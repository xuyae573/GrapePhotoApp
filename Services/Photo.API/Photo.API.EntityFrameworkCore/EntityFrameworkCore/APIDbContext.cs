using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Photo.API.EntityFrameworkCore
{
    public class APIDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public APIDbContext(DbContextOptions<APIDbContext> options) 
            : base(options)
        {

        }
    }
}
