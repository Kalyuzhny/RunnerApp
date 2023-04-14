using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Runner.Infstrastructure;

namespace Runner.Api.Infrastructure
{
    public class RunnerDbContextFactory : IDesignTimeDbContextFactory<RunnerDbContext>
    {
        public RunnerDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RunnerDbContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Runner.Api"));

            return new RunnerDbContext(optionsBuilder.Options);
        }
    }
}
