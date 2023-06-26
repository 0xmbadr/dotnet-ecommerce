using Core.Interfaces;

namespace DataAccess.Repos
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ApplicationDBContext context) { }
    }
}
