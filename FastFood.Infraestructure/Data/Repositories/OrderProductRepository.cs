using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.Data.Repositories
{
    public class OrderProductRepository : RepositoryBase<OrderProduct, AppDbContext>, IOrderProductRepository
    {
        public OrderProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<int> GetOrdersCount(long id) =>
            _context.OrderProducts.Include(r => r.Order)
                    .Where(r => r.ProductId == id && r.Order.Status != OrderStatusType.Canceled)
                    .SumAsync(s => s.Quantity);

    }
}
