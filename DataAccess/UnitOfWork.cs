using Core.Interfaces;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository => throw new NotImplementedException();
    }
}
