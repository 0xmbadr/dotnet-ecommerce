using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket?> CreateBasket(int userId);
        Task<Basket?> RetrieveBasket(int userId);
    }
}
