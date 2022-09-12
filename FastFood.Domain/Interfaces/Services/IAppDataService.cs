using FastFood.Domain.Interfaces.Repositories;

namespace FastFood.Domain.Interfaces.Services
{
    public interface IAppDataService
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderProductRepository OrderProduct { get; }
        Task SaveChangesAsync();
    }
}
