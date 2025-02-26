using backend.Logic.Models;
using backend.Logic.Repositories;
using backend.Logic.Services;
using backend.Services.EncryptionStrategy;

namespace backend.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _repository;

        public PasswordService(IPasswordRepository repository)
        {
            _repository = repository;
        }

        public async Task<Password> CreatePassword(Password password, IEncryptionStrategy strategy)
        {
            string encryptedPassword = strategy.Encrypt(password.EncryptedPassword);
            password.EncryptedPassword = encryptedPassword;

            return await _repository.Create(password);
        }

        public async Task<bool> DeletePassword(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<List<Password>> GetAllPasswords()
        {
            return await _repository.ReadAll();
        }

        public async Task<Password> GetPasswordById(int id)
        {
            return await _repository.ReadById(id);
        }
    }
}
