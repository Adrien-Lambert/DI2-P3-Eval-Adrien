using backend.Logic.Models;

namespace backend.Logic.Repositories
{
    public interface IPasswordRepository
    {
        Task<Password> Create(Password password);

        Task<List<Password>> ReadAll();

        Task<bool> Delete(int id);
        Task<Password> ReadById(int id);
    }
}
