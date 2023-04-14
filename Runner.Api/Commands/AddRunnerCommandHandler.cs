using MediatR;
using Runner.Domain.AgregatesModel;

namespace Runner.Api.Commands
{
    public class AddRunnerCommandHandler : IRequestHandler<AddRunnerCommand, bool>
    {
        private readonly IRunnerRepository _runnerRepository;
        public AddRunnerCommandHandler(IRunnerRepository runner)
        {
            _runnerRepository = runner;
        }

        public async Task<bool> Handle(AddRunnerCommand request, CancellationToken cancellationToken)
        {
            var model = new RunnerModel(request.RunnerId, 
                request.Name, 
                request.Surname, 
                request.Age, 
                request.Rank);

            await _runnerRepository.AddAsync(model);

            return true;
        }

        
    }
}
