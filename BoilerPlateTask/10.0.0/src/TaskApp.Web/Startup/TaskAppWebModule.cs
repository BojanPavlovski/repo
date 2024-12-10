using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TaskApp.Configuration;
using TaskApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;

namespace TaskApp.Web.Startup
{
    [DependsOn(
        typeof(TaskAppApplicationModule),
        typeof(TaskAppEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule))]
    public class TaskAppWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public TaskAppWebModule(IWebHostEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(TaskAppConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<TaskAppNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(TaskAppApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TaskAppWebModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TaskAppWebModule).Assembly);
        }
    }
}
