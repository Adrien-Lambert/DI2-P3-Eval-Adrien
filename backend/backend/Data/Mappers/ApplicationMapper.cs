using backend.Data.DTO;
using backend.Logic.Models;

namespace backend.Data.Mappers
{
    /// <summary>
    /// A static class that provides methods for mapping between Application entities and DTO (Data Transfer Object) representations.
    /// </summary>
    public static class ApplicationMapper
    {
        /// <summary>
        /// Maps an <see cref="ApplicationCreationDto"/> to an <see cref="Application"/> entity.
        /// </summary>
        /// <param name="applicationDto">The <see cref="ApplicationCreationDto"/> object containing application creation details.</param>
        /// <returns>An <see cref="Application"/> entity with properties populated from the <see cref="ApplicationCreationDto"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="applicationDto"/> is null.</exception>
        public static Application ToApplication(ApplicationCreationDto applicationDto)
        {
            if (applicationDto == null)
            {
                throw new ArgumentNullException(nameof(applicationDto), "ApplicationCreationDto cannot be null");
            }

            return new Application
            {
                ApplicationName = applicationDto.ApplicationName,
                ApplicationType = applicationDto.ApplicationType
            };
        }

        /// <summary>
        /// Maps an <see cref="Application"/> entity to an <see cref="ApplicationReadDto"/>.
        /// </summary>
        /// <param name="application">The <see cref="Application"/> object to be mapped.</param>
        /// <returns>An <see cref="ApplicationReadDto"/> containing the application information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="application"/> is null.</exception>
        public static ApplicationReadDto ToApplicationReadDTO(Application application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application), "Application cannot be null");
            }

            return new ApplicationReadDto
            {
                ApplicationId = application.ApplicationId,
                ApplicationName = application.ApplicationName,
                ApplicationType = application.ApplicationType
            };
        }
    }
}
