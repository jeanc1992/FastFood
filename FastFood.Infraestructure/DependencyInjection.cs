using FastFood.Domain.Interfaces.Repositories;
using FastFood.Domain.Interfaces.Services;
using FastFood.Infrastructure.Data;
using FastFood.Infrastructure.Data.Repositories;
using FastFood.Infrastructure.Middleware;
using FastFood.Infrastructure.Models.Logging;
using FastFood.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddRepos().AddServices();
            return services;
        }
        public static List<FileToLog> GetInfrastructureLogs()
        {
            return new List<FileToLog>
            {

            };
        }
        public static IApplicationBuilder UseAppExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection service, string dbName)
        {
            service.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(dbName);
            });

            return service;
        }

        private static IServiceCollection AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDataService, AppDataService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
