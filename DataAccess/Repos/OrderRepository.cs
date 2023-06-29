using Core.Entities;
using Core.Interfaces;

namespace DataAccess.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Order Add(Order order)
        {
            _context.Orders.Add(order);

            return order;
        }
    }
}
