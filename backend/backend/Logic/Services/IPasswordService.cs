using backend.Logic.Models;
using backend.Services.EncryptionStrategy;

namespace backend.Logic.Services
{
    public interface IPasswordService
    {
        /// <summary>
        /// Creates a new password.
        /// </summary>
        /// <param name="password">The password object to create.</param>
        /// <param name="strategy">The strategy to use to encrypt the password</param>
        /// <returns>The created password object.</returns>
        Task<Password> CreatePassword(Password password, IEncryptionStrategy strategy);

        /// <summary>
        /// Retrieves all passwords.
        /// </summary>
        /// <returns>A list of passwords.</returns>
        Task<List<Password>> GetAllPasswords();

        /// <summary>
        /// Creates a new password.
        /// </summary>
        /// <param name="id">The id of th password to delete.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        Task<bool> DeletePassword(int id);

        /// <summary>
        /// Retrieves an password by its ID.
        /// </summary>
        /// <param name="id">The password ID.</param>
        /// <returns>The password object if found; otherwise, null.</returns>
        Task<Password> GetPasswordById(int id);
    }
}
