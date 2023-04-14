using Microsoft.Extensions.Logging;
using Runner.Domain.AgregatesModel;

namespace Runner.Api.Queries
{
    public class RunnerQueries : IRunnerQueries
    {
        private readonly ILogger<RunnerQueries> _logger;

        private IRunnerRepository _runnerRepository { get; set; }
        public RunnerQueries(IRunnerRepository runnerRepository, ILogger<RunnerQueries> logger)
        {
            _runnerRepository = runnerRepository ?? throw new ArgumentNullException(nameof(runnerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RunnerModel> GetRunnerAsync(int id)
        {
            _logger.LogDebug($"Getting runners with id={id}.");

            return await _runnerRepository.GetAsync(id);
        }

        public async Task<IEnumerable<RunnerModel>> GetRunnersAsync()
        {
            _logger.LogDebug("Getting all runners");

            return await _runnerRepository.GetAsync();
        }
    }
}
