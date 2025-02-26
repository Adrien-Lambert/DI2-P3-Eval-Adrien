using backend.Data.DatabaseContext;
using backend.Logic.Models;
using backend.Logic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly BackendDbContext _context;

        public PasswordRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Password> Create(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
            return password;
        }

        public async Task<bool> Delete(int id)
        {
            var password = await _context.Passwords.FirstOrDefaultAsync(p => p.PasswordId == id);
            if (password == null)
            {
                return false;
            }

            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Password>> ReadAll()
        {
            return await _context.Passwords.ToListAsync();
        }

        public async Task<Password> ReadById(int id)
        {
            return await _context.Passwords.FirstOrDefaultAsync(p => p.PasswordId == id);
        }
    }
}
