namespace TaskChallenge.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
{
    public TaskDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
        optionsBuilder.UseSqlite("Data Source=../TaskChallenge.Api/TaskChallenge.db");

        return new TaskDbContext(optionsBuilder.Options);
    }
}
