using Maps.src.Maps.Core.Interfaces;
using Maps.src.Maps.Core.Models;
using Maps.src.Maps.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Maps.src.Maps.Infrastructure.Repositories
{
    public class UserRepository : BasicRepo<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<User>> GetAdminsAsync()
        {
            return await _dbSet
                .Where(u => u.UserRole.Name == "Admin")
                .ToListAsync();
        }
    }
}
