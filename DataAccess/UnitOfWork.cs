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
            Baskets = new BasketRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public IProductRepository Products { get; private set; }

        public IBasketRepository Baskets { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
