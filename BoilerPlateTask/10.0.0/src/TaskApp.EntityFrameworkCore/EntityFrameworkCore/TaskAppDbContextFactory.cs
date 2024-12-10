using TaskApp.Configuration;
using TaskApp.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskApp.EntityFrameworkCore;

/* This class is needed to run EF Core PMC commands. Not used anywhere else */
public class TaskAppDbContextFactory : IDesignTimeDbContextFactory<TaskAppDbContext>
{
    public TaskAppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TaskAppDbContext>();
        var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

        DbContextOptionsConfigurer.Configure(
            builder,
            configuration.GetConnectionString(TaskAppConsts.ConnectionStringName)
        );

        return new TaskAppDbContext(builder.Options);
    }
}