using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tournament.Domain;
using Tournament.Domain.AgregatesModel;
using System.Linq;
using MediatR;

namespace Tournament.Infstrastructure.Repositories
{
    public class TournamentRepository
    : ITournamentRepository
    {
        private readonly TournamentDbContext _context;
        private readonly IMediator _mediator;

        public IUnitOfWork UnitOfWork => _context;

        public TournamentRepository(IDesignTimeDbContextFactory<TournamentDbContext> contextFactory, IMediator mediator)
        {
            var contextFactoryLocal = contextFactory ?? throw new ArgumentNullException(nameof(IDesignTimeDbContextFactory<TournamentDbContext>));
            _context = contextFactoryLocal.CreateDbContext(new string[] { });
            _mediator = mediator ?? throw new ArgumentNullException(nameof(TournamentRepository));
        }

        public async Task<bool> AddAsync(TournamentModel tournament)
        {
            try
            {
                await _context.Tournaments.AddAsync(tournament);

                await _context.SaveEntitiesAsync();

                return true;
            }
            catch(Exception ex)
            {
                //log exception;
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int tournamentId)
        {
            try
            {
                var tournament = await _context.Tournaments.FirstOrDefaultAsync(o => o.Id == tournamentId);

                if (tournament != null)
                {
                    var result = _context.Remove(tournament);

                    await _context.SaveEntitiesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                //log exception;
                return false;
            }
        }

        public async Task<TournamentModel> GetAsync(int runnerId)
        {
            //out of scope for demo
            return null;
        }

        public async Task<IEnumerable<TournamentModel>> GetAsync()
        {
            //out of scope for demo
            return null;
        }

        public void Update(TournamentModel runner)
        {
            //out of scope for demo
        }

        
    }
}
