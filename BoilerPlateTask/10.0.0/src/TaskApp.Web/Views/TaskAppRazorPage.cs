using Abp.AspNetCore.Mvc.Views;

namespace TaskApp.Web.Views
{
    public abstract class TaskAppRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected TaskAppRazorPage()
        {
            LocalizationSourceName = TaskAppConsts.LocalizationSourceName;
        }
    }
}
