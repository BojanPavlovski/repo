using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.TestBase;
using TaskApp.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TaskApp.Tests;

[DependsOn(
    typeof(TaskAppApplicationModule),
    typeof(TaskAppEntityFrameworkCoreModule),
    typeof(AbpTestBaseModule)
    )]
public class TaskAppTestModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        SetupInMemoryDb();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TaskAppTestModule).GetAssembly());
    }

    private void SetupInMemoryDb()
    {
        var services = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase();

        var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(
            IocManager.IocContainer,
            services
        );

        var builder = new DbContextOptionsBuilder<TaskAppDbContext>();
        builder.UseInMemoryDatabase("Test").UseInternalServiceProvider(serviceProvider);

        IocManager.IocContainer.Register(
            Component
                .For<DbContextOptions<TaskAppDbContext>>()
                .Instance(builder.Options)
                .LifestyleSingleton()
        );
    }
}