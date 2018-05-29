using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Notification.API.EntityFrameworkCore
{
    [DependsOn(
        typeof(APICoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class APIEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(APIEntityFrameworkCoreModule).GetAssembly());
        }
    }
}