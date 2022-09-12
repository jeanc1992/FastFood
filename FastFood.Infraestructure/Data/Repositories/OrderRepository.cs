using FastFood.Domain.Dto.Orders.Response;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace FastFood.Infrastructure.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Order>> GetAllOrders(OrderStatusType? status)
        {
            var query = _context.Orders.Include(r=>r.OrderProduct).AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Order> GetOrder(long id)
        {
            var order = await _context.Orders.Include(r => r.OrderProduct).FirstOrDefaultAsync(r => r.Id == id);
            
            if (order == null)
                throw new NotFoundException($"Order id = {id} not found");

            return order;
        }
    }
}
