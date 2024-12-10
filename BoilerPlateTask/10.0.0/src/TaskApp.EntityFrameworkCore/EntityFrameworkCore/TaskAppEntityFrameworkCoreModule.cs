using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TaskApp.EntityFrameworkCore;

[DependsOn(
    typeof(TaskAppCoreModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class TaskAppEntityFrameworkCoreModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TaskAppEntityFrameworkCoreModule).GetAssembly());
    }
}