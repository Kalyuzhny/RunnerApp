using MediatR;
using Microsoft.AspNetCore.Mvc;
using Runner.Api.Commands;
using Runner.Domain.AgregatesModel;
using Runner.Infstrastructure.Repositories;

namespace Runner.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RunnerController : ControllerBase
    {
        private readonly ILogger<RunnerController> _logger;
        private readonly IRunnerRepository _runnerRepository;
        private readonly IMediator _mediator;

        public RunnerController(ILogger<RunnerController> logger, IRunnerRepository runnerRepository, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(RunnerController));
            _runnerRepository = runnerRepository ?? throw new ArgumentNullException(nameof(IRunnerRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(Mediator));
        }

        [Route("{runnerId:int}")]
        [HttpGet]
        public async Task<ActionResult> GetRunnerAsync(int runnerId)
        {
            _logger.LogInformation($"Calling GetRunnerAsync action with id={runnerId}");

            var runner = await _runnerRepository.GetAsync(runnerId);

            return Ok(runner);
        }

        [HttpGet]
        public async Task<ActionResult> GetRunnersAsync()
        {
            _logger.LogInformation($"Calling GetRunnersAsync action");

            var runners = await _runnerRepository.GetAsync();

            return Ok(runners);
        }

        [HttpPost]
        public async Task<ActionResult> AddRunnerAsync([FromBody] AddRunnerCommand addRunnerCommand)
        {
            _logger.LogInformation($"Calling AddRunnerAsync action by using mediatr");

            await _mediator.Send(addRunnerCommand);

            return Ok();
        }
    }
}