using System.Collections.Generic;

namespace Runner.Domain.AgregatesModel
{
    public interface IRunnerRepository : IRepository<RunnerModel>
    {
        Task<bool> AddAsync(RunnerModel runner);

        void Update(RunnerModel runner);

        Task<RunnerModel> GetAsync(int runnerId);

        Task<IEnumerable<RunnerModel>> GetAsync();
    }
}
