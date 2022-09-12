using FastFood.Domain.Dto.Orders.Response;
using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces.Repositories
{
    public interface IOrderProductRepository : IRepositoryBase<OrderProduct>
    {
        Task<int> GetOrdersCount(long id);
    }
}
