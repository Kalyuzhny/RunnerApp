using MediatR;
using Microsoft.EntityFrameworkCore;
using Runner.Domain;
using Runner.Infstrastructure.EntityConfiguration;

namespace Runner.Infstrastructure
{
    public class RunnerDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "runner";

        public DbSet<Domain.AgregatesModel.RunnerModel> Runners { get; set; }

        public RunnerDbContext(DbContextOptions<RunnerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RunnerEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}