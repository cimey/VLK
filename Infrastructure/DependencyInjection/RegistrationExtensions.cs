using Domain.Repositories;
using Infrastructure.DatabaseContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection RegisterInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientStockPositionRepository, ClientStockPositionRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockOrderRepository, StockOrderRepository>();

            services.AddDbContext<VLKContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));
            return services;
        }
    }
}
