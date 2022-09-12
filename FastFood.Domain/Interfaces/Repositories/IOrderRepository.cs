using FastFood.Domain.Entities;
using FastFood.Domain.Enums;

namespace FastFood.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order> GetOrder(long id);
        Task<List<Order>> GetAllOrders(OrderStatusType? status);
    }
}
