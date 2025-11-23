using Application.Clients;
using Application.Stock;
using Application.Trading;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ITradingService, TradingService>();
            services.AddScoped<IStockService, StockService>();
            return services;
        }
    }
}
