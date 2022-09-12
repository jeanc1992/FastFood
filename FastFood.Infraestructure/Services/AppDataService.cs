using FastFood.Domain.Interfaces.Repositories;
using FastFood.Domain.Interfaces.Services;
using FastFood.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace FastFood.Infrastructure.Services
{
    public class AppDataService : IAppDataService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<AppDataService> _logger;

        public IProductRepository Products { get; }

        public IOrderRepository Orders { get; }

        public IOrderProductRepository OrderProduct { get; }

        public AppDataService(
            AppDbContext dbContext,
            ILogger<AppDataService> logger,
            IProductRepository products,
            IOrderRepository order,
            IOrderProductRepository orderProduct)
        {
            _dbContext = dbContext;
            _logger = logger;
            Products = products;
            Orders = order;
            OrderProduct = orderProduct;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                var changesMade = await _dbContext.SaveChangesAsync();

                if (changesMade == 0)
                    _logger.LogWarning("There weren't changes made in this context");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while trying to save the changes");
                throw;
            }
        }
    }
}
