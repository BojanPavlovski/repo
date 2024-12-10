using Abp.Modules;
using Abp.Reflection.Extensions;
using TaskApp.Localization;

namespace TaskApp;

public class TaskAppCoreModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Auditing.IsEnabledForAnonymousUsers = true;

        TaskAppLocalizationConfigurer.Configure(Configuration.Localization);

        Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = TaskAppConsts.DefaultPassPhrase;
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TaskAppCoreModule).GetAssembly());
    }
}