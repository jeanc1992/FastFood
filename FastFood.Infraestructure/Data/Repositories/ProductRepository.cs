using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces.Repositories;

namespace FastFood.Infrastructure.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
