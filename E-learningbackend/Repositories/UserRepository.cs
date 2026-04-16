using Elearningbackend.Data;
using Elearningbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Elearningbackend.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ElearningDbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}