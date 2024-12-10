using Abp.Application.Services;

namespace TaskApp;

/// <summary>
/// Derive your application services from this class.
/// </summary>
public abstract class TaskAppAppServiceBase : ApplicationService
{
    protected TaskAppAppServiceBase()
    {
        LocalizationSourceName = TaskAppConsts.LocalizationSourceName;
    }
}