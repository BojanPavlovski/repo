using Abp.TestBase;
using TaskApp.EntityFrameworkCore;
using TaskApp.Tests.TestDatas;
using System;
using System.Threading.Tasks;

namespace TaskApp.Tests;

public class TaskAppTestBase : AbpIntegratedTestBase<TaskAppTestModule>
{
    public TaskAppTestBase()
    {
        UsingDbContext(context => new TestDataBuilder(context).Build());
    }

    protected virtual void UsingDbContext(Action<TaskAppDbContext> action)
    {
        using (var context = LocalIocManager.Resolve<TaskAppDbContext>())
        {
            action(context);
            context.SaveChanges();
        }
    }

    protected virtual T UsingDbContext<T>(Func<TaskAppDbContext, T> func)
    {
        T result;

        using (var context = LocalIocManager.Resolve<TaskAppDbContext>())
        {
            result = func(context);
            context.SaveChanges();
        }

        return result;
    }

    protected virtual async Task UsingDbContextAsync(Func<TaskAppDbContext, Task> action)
    {
        using (var context = LocalIocManager.Resolve<TaskAppDbContext>())
        {
            await action(context);
            await context.SaveChangesAsync(true);
        }
    }

    protected virtual async Task<T> UsingDbContextAsync<T>(Func<TaskAppDbContext, Task<T>> func)
    {
        T result;

        using (var context = LocalIocManager.Resolve<TaskAppDbContext>())
        {
            result = await func(context);
            context.SaveChanges();
        }

        return result;
    }
}
