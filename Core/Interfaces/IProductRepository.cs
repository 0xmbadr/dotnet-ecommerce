using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetSingleProduct(int id);
        Task<Product> CreateProduct(Product product);
    }
}
