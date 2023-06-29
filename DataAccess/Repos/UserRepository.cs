using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserWithAddress(string username)
        {
            return await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
