using MediatR;
using System.Runtime.Serialization;
using Tournament.Api.Requests;

namespace Tournament.Api.Commands
{
    [DataContract]
    public class AddTournamentRunnersCommand : IRequest<bool>
    {
        [DataMember]
        public RunnersRequestModel[] Runners { get; private set; }

        public AddTournamentRunnersCommand(RunnersRequestModel[] runners)
        {
            Runners = runners;
        }
    }
}
