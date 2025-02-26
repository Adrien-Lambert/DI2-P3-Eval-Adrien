using backend.Logic.Models;

namespace backend.Logic.Repositories
{
    public interface IApplicationRepository
    {
        Task<Application> Create(Application application);

        Task<List<Application>> ReadAll();

        Task<Application> ReadById(int id);

    }
}
