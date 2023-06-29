using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        Order Add(Order order);
        Task<List<OrderDto>> GetOrders(int userId);
        Task<OrderDto> GetSingleOrder(int userId, int orderId);
    }
}
