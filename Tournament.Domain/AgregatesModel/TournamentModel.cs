namespace Tournament.Domain.AgregatesModel
{
    public class TournamentModel : IAggregateRoot
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Country { get; private set; }

        public IList<TournamentRunnerModel> Runners { get; private set; }

        public TournamentModel(int id, string title, string country, List<TournamentRunnerModel> runners)
        {
            Id = id;
            Title = title;
            Country = country;

            Runners = runners;
        }

        public void AddTournamentRunner(int tournamentId, int runnerId)
        {
            var existingRunner = Runners.Where(o => o.RunnerId == runnerId)
                .SingleOrDefault();

            if (existingRunner != null)
            {
                // some potential logic to skip adding
            }
            else
            {
                var tournamentRunner = new TournamentRunnerModel(tournamentId, runnerId);
                Runners.Add(tournamentRunner);
            }
        }

    }
}
