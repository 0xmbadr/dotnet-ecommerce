using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        Order Add(Order order);
    }
}
