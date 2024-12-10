using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TaskApp;

[DependsOn(
    typeof(TaskAppCoreModule),
    typeof(AbpAutoMapperModule))]
public class TaskAppApplicationModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TaskAppApplicationModule).GetAssembly());
    }
}