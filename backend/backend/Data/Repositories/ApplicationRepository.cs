using backend.Data.DatabaseContext;
using backend.Logic.Models;
using backend.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly BackendDbContext _context;

        public ApplicationRepository(BackendDbContext context) {
            _context = context;
        }

        public async Task<Application> Create(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<List<Application>> ReadAll()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application> ReadById(int id)
        {
            return await _context.Applications.FirstOrDefaultAsync(a => a.ApplicationId == id);

        }
    }
}
