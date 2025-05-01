using EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Abstraction;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.EventBus;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderServiceDbContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddCap(x =>
            {
                x.UseEntityFramework<OrderServiceDbContext>();
                x.UseKafka("localhost:9092");
                x.UseDashboard();
            });
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            return services;
        }
    }
}
