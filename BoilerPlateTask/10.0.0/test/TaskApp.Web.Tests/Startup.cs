using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Dependency;
using TaskApp.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace TaskApp.Web.Tests;

public class Startup
{
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddEntityFrameworkInMemoryDatabase();

        services.AddMvc();

        //Configure Abp and Dependency Injection
        return services.AddAbp<TaskAppWebTestModule>(options =>
        {
            options.SetupTest();
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        UseInMemoryDb(app.ApplicationServices);

        app.UseAbp(); //Initializes ABP framework.

        app.UseExceptionHandler("/Error");

        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });
    }

    private void UseInMemoryDb(IServiceProvider serviceProvider)
    {
        var builder = new DbContextOptionsBuilder<TaskAppDbContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);
        var options = builder.Options;

        var iocManager = serviceProvider.GetRequiredService<IIocManager>();

        iocManager.IocContainer
            .Register(
                Component.For<DbContextOptions<TaskAppDbContext>>()
                .Instance(options)
                .LifestyleSingleton()
            );
    }
}
