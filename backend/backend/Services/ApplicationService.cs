using backend.Logic.Models;
using backend.Logic.Repositories;
using backend.Logic.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;

        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Application> CreateApplication(Application application)
        {
            return await _repository.Create(application);
        }

        public async Task<List<Application>> GetAllApplications()
        {
            return await _repository.ReadAll();
        }

        public async Task<Application> GetApplicationById(int id)
        {
            return await _repository.ReadById(id);
        }
    }
}
