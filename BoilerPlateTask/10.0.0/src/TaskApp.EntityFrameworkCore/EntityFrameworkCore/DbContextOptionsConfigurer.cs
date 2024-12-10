using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TaskApp.EntityFrameworkCore;

public static class DbContextOptionsConfigurer
{
    public static void Configure(
        DbContextOptionsBuilder<TaskAppDbContext> dbContextOptions,
        string connectionString
        )
    {
        /* This is the single point to configure DbContextOptions for TaskAppDbContext */
        dbContextOptions.UseSqlServer(connectionString);
        dbContextOptions.ConfigureWarnings(warnings => { warnings.Ignore(RelationalEventId.PendingModelChangesWarning); });
    }
}
