using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tournament.Infstrastructure;

namespace Tournament.Api.Infrastructure
{
    public class TournamentDbContextFactory : IDesignTimeDbContextFactory<TournamentDbContext>
    {
        public TournamentDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TournamentDbContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Tournament.Api"));

            return new TournamentDbContext(optionsBuilder.Options);
        }
    }
}
