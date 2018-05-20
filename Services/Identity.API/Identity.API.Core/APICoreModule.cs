using Abp.Modules;
using Abp.Reflection.Extensions;
using Identity.API.Localization;

namespace Identity.API
{
    public class APICoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            APILocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(APICoreModule).GetAssembly());
        }
    }
}