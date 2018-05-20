using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Notification.API
{
    [DependsOn(
        typeof(APICoreModule), 
        typeof(AbpAutoMapperModule))]
    public class APIApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(APIApplicationModule).GetAssembly());
        }
    }
}