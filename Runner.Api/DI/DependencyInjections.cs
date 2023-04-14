using Microsoft.EntityFrameworkCore.Design;
using Runner.Api.Infrastructure;
using Runner.Domain;
using Runner.Domain.AgregatesModel;
using Runner.Infstrastructure;
using Runner.Infstrastructure.Repositories;

namespace Runner.Api.DI
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IRunnerRepository, RunnerRepository>();
            services.AddTransient<IDesignTimeDbContextFactory<RunnerDbContext>, RunnerDbContextFactory>();

            return services;
        }
    }
}
