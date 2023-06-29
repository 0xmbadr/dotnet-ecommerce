using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.MappingExtensions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<OrderDto>> GetOrders(int userId)
        {
            var orders = await _context.Orders
                .ProjectOrderToOrderDto()
                .Where(x => x.BuyerId == userId)
                .ToListAsync();

            return orders;
        }

        public async Task<OrderDto> GetSingleOrder(int userId, int orderId)
        {
            var order = await _context.Orders
                .ProjectOrderToOrderDto()
                .FirstOrDefaultAsync(x => x.BuyerId == userId && x.Id == orderId);

            return order;
        }
    }
}
