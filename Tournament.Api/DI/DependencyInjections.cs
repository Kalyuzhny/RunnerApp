using MediatR;
using Microsoft.EntityFrameworkCore.Design;
using Tournament.Api.Commands;
using Tournament.Api.Infrastructure;
using Tournament.Domain.AgregatesModel;
using Tournament.Infstrastructure;
using Tournament.Infstrastructure.Repositories;

namespace Tournament.Api.DI
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<ITournamentRepository, TournamentRepository>();
            services.AddTransient<IRequestHandler<AddTournamentRunnersCommand, bool>, AddTournamentRunnersCommandHandler>();
            services.AddTransient<IDesignTimeDbContextFactory<TournamentDbContext>, TournamentDbContextFactory>();

            return services;
        }
    }
}
