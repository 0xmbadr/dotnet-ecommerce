namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        IBasketRepository Baskets { get; }

        int Complete();
    }
}
