using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournament.Domain;
using Tournament.Domain.AgregatesModel;
using Tournament.Infstrastructure.EntityConfiguration;

namespace Tournament.Infstrastructure
{
    public class TournamentDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "tournament";
        
        public DbSet<TournamentModel> Tournaments { get; set; }

        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TournamentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentRunnerEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);            

            return true;
        }
    }
}