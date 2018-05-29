using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Photo.API.Configuration;
using Photo.API.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Photo.API.Web.Startup
{
    [DependsOn(
        typeof(APIApplicationModule), 
        typeof(APIEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class APIWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public APIWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(APIConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<APINavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(APIApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(APIWebModule).GetAssembly());
        }
    }
}