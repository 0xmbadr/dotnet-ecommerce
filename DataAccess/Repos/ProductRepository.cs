using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetSingleProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
