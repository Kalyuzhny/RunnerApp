namespace Tournament.Domain.AgregatesModel
{
    public interface ITournamentRepository : IRepository<TournamentModel>
    {
        Task<bool> AddAsync(TournamentModel tournament);

        Task<bool> DeleteAsync(int tournamentId);

        void Update(TournamentModel tournament);

        Task<TournamentModel> GetAsync(int tournamentId);

        Task<IEnumerable<TournamentModel>> GetAsync();
    }
}
