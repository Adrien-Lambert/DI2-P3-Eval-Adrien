using backend.Logic.Models;

namespace backend.Logic.Services
{
    public interface IApplicationService
    {
        /// <summary>
        /// Creates a new application.
        /// </summary>
        /// <param name="application">The application object to create.</param>
        /// <returns>The created application object.</returns>
        Task<Application> CreateApplication(Application application);

        /// <summary>
        /// Retrieves all applications.
        /// </summary>
        /// <returns>A list of applications.</returns>
        Task<List<Application>> GetAllApplications();

        /// <summary>
        /// Retrieves an application by its ID.
        /// </summary>
        /// <param name="id">The application ID.</param>
        /// <returns>The application object if found; otherwise, null.</returns>
        Task<Application> GetApplicationById(int id);
    }
}
