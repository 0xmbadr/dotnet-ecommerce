using Core.Interfaces;
using DataAccess.Repos;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public IProductRepository Products { get; private set; }
    }
}
