namespace Runner.Domain.AgregatesModel
{
    public class RunnerModel : IAggregateRoot
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Age { get; private set; }
        public int Rank { get; private set; }

        public RunnerModel(int id, string name, string surname, int age, int rank)
        {
            ValidateAge(age);

            Id = id;
            Name = name;
            Surname = surname;  
            Age = age;  
            Rank = rank;
        }

        private void ValidateAge(int age)
        {
            if (age < 18)
                throw new ArgumentException("It is only allowed to have adult runner");
        }
    }
}
