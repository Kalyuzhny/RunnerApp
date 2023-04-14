using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tournament.Api.Commands;
using Tournament.Api.Requests;
using Tournament.Domain.AgregatesModel;

namespace Runner.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMediator _mediator;

        public TournamentController(ILogger<TournamentController> logger, 
            ITournamentRepository tournamentRepository,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(TournamentController));
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException(nameof(ITournamentRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
        }

        [HttpPost]
        public async Task<ActionResult> AddTurnamentsAsync([FromBody] TournamentRequestModel model)
        {
            _logger.LogInformation($"Calling AddTurnamentsAsync action");

            var status = await _tournamentRepository.AddAsync(new TournamentModel(
                model.Id,
                model.Country,
                model.Title,
                model.Runners.Select(t=> new TournamentRunnerModel(model.Id, t.Id)).ToList()));

            try
            {
                var addTournamentRunnersCommand = new AddTournamentRunnersCommand(model.Runners.ToArray());
                var result = await _mediator.Send(addTournamentRunnersCommand);
                                
                if (!result)
                {
                    //Compensation transaction. if Adding of runners is failed application has to rollback tournament.
                    RollbackTournament(model.Id);
                    throw new ApplicationException("Some exception message for client that tournament was not added");
                }
            }
            catch (Exception ex)
            {
                RollbackTournament(model.Id);
                _logger.LogError(ex.Message);
                throw new ApplicationException("Some exception message for client that tournament was not added");
            }
            return Ok();
        }

        private async void RollbackTournament(int tournamentId)
        {
            //Compensational transaction. if Adding of runners is failed application has to rollback tournament.
            await _tournamentRepository.DeleteAsync(tournamentId);
        }



    }
}