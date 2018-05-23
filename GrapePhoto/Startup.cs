using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using GrapePhoto.Proxy;
using GrapePhoto.Web.Models.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrapePhoto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.AddDistributedMemoryCache();

            var accessKey = Configuration.GetValue<string>("AccessKey");
            var secretKey = Configuration.GetValue<string>("SecretKey");

            var keys = new CredentialProfileOptions
            {
                AccessKey = accessKey,
                SecretKey = secretKey
            };
            var profile = new CredentialProfile("basic_profile", keys)
            {
                Region = RegionEndpoint.USEast1
            };
            var netSDKFile = new NetSDKCredentialsFile();
            netSDKFile.RegisterProfile(profile);


            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddIdentity<User, IdentityRole>();
            services.Configure<AppSettings>(Configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login/";
                options.AccessDeniedPath = "/Account/Forbidden/";
            });
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddTransient<IAccountClient, AccountClient>(c =>
            {
                return new AccountClient(identityUrl);
            });

            services.AddTransient<IPictureService, AWSPictureService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
