using backend.Data.DTO;
using backend.Logic.Models;

namespace backend.Data.Mappers
{
    /// <summary>
    /// A static class that provides methods for mapping between Password entities and DTO (Data Transfer Object) representations.
    /// </summary>
    public static class PasswordMapper
    {
        /// <summary>
        /// Maps an <see cref="PasswordCreationDto"/> to an <see cref="Password"/> entity.
        /// </summary>
        /// <param name="passwordDto">The <see cref="PasswordCreationDto"/> object containing password creation details.</param>
        /// <returns>An <see cref="Password"/> entity with properties populated from the <see cref="PasswordCreationDto"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="passwordDto"/> is null.</exception>
        public static Password ToPassword(PasswordCreationDto passwordDto)
        {
            if (passwordDto == null)
            {
                throw new ArgumentNullException(nameof(passwordDto), "PasswordCreationDto cannot be null");
            }

            return new Password
            {
                AccountName = passwordDto.AccountName,
                EncryptedPassword = passwordDto.EncryptedPassword,
                ApplicationId = passwordDto.ApplicationId,
            };
        }

        /// <summary>
        /// Maps an <see cref="Password"/> entity to an <see cref="PasswordReadDto"/>.
        /// </summary>
        /// <param name="password">The <see cref="Password"/> object to be mapped.</param>
        /// <returns>An <see cref="PasswordReadDto"/> containing the password information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="password"/> is null.</exception>
        public static PasswordReadDto ToPasswordReadDTO(Password password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null");
            }

            return new PasswordReadDto
            {
                PasswordId = password.PasswordId,
                AccountName = password.AccountName,
                EncryptedPassword = password.EncryptedPassword,
                Application= ApplicationMapper.ToApplicationReadDTO(password.Application)
            };
        }
    }
}
