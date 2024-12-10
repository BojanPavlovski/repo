using System;
using TaskApp.EntityFrameworkCore;

namespace TaskApp.Tests.TestDatas;

public class TestDataBuilder
{
    private readonly TaskAppDbContext _context;

    public TestDataBuilder(TaskAppDbContext context)
    {
        _context = context;
    }

    public void Build()
    {
        //create test data here...
        var neo = new Person("Neo");
        _context.People.Add(neo);
        _context.SaveChanges();

        _context.Tasks.AddRange(
            new TaskModel("Follow the white rabbit", "Follow the white rabbit in order to know the reality.", neo.Id),
            new TaskModel("Clean your room") { State = TaskState.Completed }
            );
    }

}