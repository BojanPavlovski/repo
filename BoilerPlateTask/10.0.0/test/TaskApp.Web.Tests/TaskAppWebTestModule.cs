using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TaskApp.Web.Startup;
namespace TaskApp.Web.Tests;

[DependsOn(
    typeof(TaskAppWebModule),
    typeof(AbpAspNetCoreTestBaseModule)
    )]
public class TaskAppWebTestModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TaskAppWebTestModule).GetAssembly());
    }
}