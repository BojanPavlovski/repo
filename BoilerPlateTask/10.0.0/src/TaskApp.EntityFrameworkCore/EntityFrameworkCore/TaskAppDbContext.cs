using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskApp.EntityFrameworkCore;

public class TaskAppDbContext : AbpDbContext
{
    //Add DbSet properties for your entities...
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<Person> People { get; set; }


    public TaskAppDbContext(DbContextOptions<TaskAppDbContext> options)
        : base(options)
    {

    }
}
