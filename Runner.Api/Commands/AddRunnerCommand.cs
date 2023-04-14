using MediatR;
using System.Runtime.Serialization;

namespace Runner.Api.Commands
{
    [DataContract]
    public class AddRunnerCommand : IRequest<bool>
    {
        [DataMember]
        public int RunnerId { get; private set; }

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Surname { get; private set; }
        [DataMember]
        public int Age { get; private set; }
        [DataMember]
        public int Rank { get; private set; }

        public AddRunnerCommand(int runnerId, string name, string surname, int age, int rank)
        {
            RunnerId = runnerId;
            Name = name;
            Surname = surname;
            Age = age;
            Rank = rank;
        }
    }
}
