using Microsoft.VisualBasic;
using Runner.Domain.AgregatesModel;

namespace Runner.Api.Queries
{
    public interface IRunnerQueries
    {
        Task<RunnerModel> GetRunnerAsync(int id);

        Task<IEnumerable<RunnerModel>> GetRunnersAsync();
    }
}
