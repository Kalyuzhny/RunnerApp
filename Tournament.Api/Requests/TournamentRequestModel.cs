namespace Tournament.Api.Requests
{
    public class TournamentRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }

        public List<RunnersRequestModel> Runners { get; set; }

        public TournamentRequestModel()
        {
            Runners = new List<RunnersRequestModel>();
        }
    }
}
