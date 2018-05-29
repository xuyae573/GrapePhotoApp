using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Identity.API
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