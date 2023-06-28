using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDBContext _context;

        public BasketRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Basket?> CreateBasket(int userId)
        {
            var basket = new Basket { BuyerId = userId };

            await _context.AddAsync(basket);

            return basket;
        }

        public async Task<Basket?> RetrieveBasket(int userId)
        {
            var basket = await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(b => b.BuyerId == userId);

            if (basket == null)
                return null;

            return basket;
        }
    }
}
