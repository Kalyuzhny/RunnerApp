namespace Tournament.Domain.AgregatesModel
{
    public class TournamentRunnerModel
    {
        public int Id { get; private set; }
        public int TournamentId { get; private set; }
        public int RunnerId { get; private set; }

        public TournamentModel TournamentModel { get; set; }

        public TournamentRunnerModel(int tournamentId, int runnerId)
        {
            TournamentId = tournamentId;
            RunnerId = runnerId;
        }
    }
}
