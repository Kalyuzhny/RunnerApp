using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Runner.Domain;
using Runner.Domain.AgregatesModel;
using System.Linq;

namespace Runner.Infstrastructure.Repositories
{
    public class RunnerRepository
    : IRunnerRepository
    {
        private readonly RunnerDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public RunnerRepository(IDesignTimeDbContextFactory<RunnerDbContext> contextFactory)
        {
            var contextFactoryLocal = contextFactory ?? throw new ArgumentNullException(nameof(IDesignTimeDbContextFactory<RunnerDbContext>));
            _context = contextFactoryLocal.CreateDbContext(new string[] { });
        }

        public async Task<bool> AddAsync(RunnerModel runner)
        {
            await _context.Runners.AddAsync(runner);
            return true;

        }

        public async Task<RunnerModel> GetAsync(int runnerId)
        {
            var runner = await _context
                                .Runners
                                .FirstOrDefaultAsync(o => o.Id == runnerId);
            if (runner == null)
            {
                runner = _context
                            .Runners
                            .Local
                            .FirstOrDefault(o => o.Id == runnerId);
            }

            return runner;
        }

        public async Task<IEnumerable<RunnerModel>> GetAsync()
        {
            return await _context.Runners.ToArrayAsync();
        }

        public void Update(RunnerModel runner)
        {
            _context.Entry(runner).State = EntityState.Modified;
        }

        
    }
}
