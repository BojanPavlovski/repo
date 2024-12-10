using Abp.AspNetCore.TestBase;
using TaskApp.EntityFrameworkCore;
using TaskApp.Tests.TestDatas;
using TaskApp.Web.Startup;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Hosting;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskApp.Web.Tests;

public abstract class TaskAppWebTestBase : AbpAspNetCoreIntegratedTestBase<Startup>
{
    protected static readonly Lazy<string> ContentRootFolder;

    static TaskAppWebTestBase()
    {
        ContentRootFolder = new Lazy<string>(WebContentDirectoryFinder.CalculateContentRootFolder, true);
    }

    protected TaskAppWebTestBase()
    {
        UsingDbContext(context => new TestDataBuilder(context).Build());
    }

    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return base
            .CreateWebHostBuilder()
            .UseContentRoot(ContentRootFolder.Value)
            .UseSetting(WebHostDefaults.ApplicationKey, typeof(TaskAppWebModule).Assembly.FullName);
    }

    #region Get response

    protected async Task<T> GetResponseAsObjectAsync<T>(string url,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode);
        return JsonSerializer.Deserialize<T>(strResponse, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    protected async Task<string> GetResponseAsStringAsync(string url,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var response = await GetResponseAsync(url, expectedStatusCode);
        return await response.Content.ReadAsStringAsync();
    }

    protected async Task<HttpResponseMessage> GetResponseAsync(string url,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var response = await Client.GetAsync(url);
        response.StatusCode.ShouldBe(expectedStatusCode);
        return response;
    }

    #endregion

    #region UsingDbContext

    protected void UsingDbContext(Action<TaskAppDbContext> action)
    {
        using (var context = IocManager.Resolve<TaskAppDbContext>())
        {
            action(context);
            context.SaveChanges();
        }
    }

    protected T UsingDbContext<T>(Func<TaskAppDbContext, T> func)
    {
        T result;

        using (var context = IocManager.Resolve<TaskAppDbContext>())
        {
            result = func(context);
            context.SaveChanges();
        }

        return result;
    }

    protected async Task UsingDbContextAsync(Func<TaskAppDbContext, Task> action)
    {
        using (var context = IocManager.Resolve<TaskAppDbContext>())
        {
            await action(context);
            await context.SaveChangesAsync(true);
        }
    }

    protected async Task<T> UsingDbContextAsync<T>(Func<TaskAppDbContext, Task<T>> func)
    {
        T result;

        using (var context = IocManager.Resolve<TaskAppDbContext>())
        {
            result = await func(context);
            context.SaveChanges();
        }

        return result;
    }

    #endregion

    #region ParseHtml

    protected IHtmlDocument ParseHtml(string htmlString)
    {
        return new HtmlParser().ParseDocument(htmlString);
    }

    #endregion
}
