using Abp.AspNetCore.Mvc.Controllers;

namespace TaskApp.Web.Controllers
{
    public abstract class TaskAppControllerBase : AbpController
    {
        protected TaskAppControllerBase()
        {
            LocalizationSourceName = TaskAppConsts.LocalizationSourceName;
        }
    }
}