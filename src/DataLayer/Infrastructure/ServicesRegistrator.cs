using Microsoft.Extensions.DependencyInjection;
using VehicleShop.DataLayer.Repositories.Implementations;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.DataLayer.Infrastructure
{
    public static class ServicesRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<IDistributorsRepository, DistributorsRepository>();
            services.AddTransient<ITransactionsRepository, TransactionsRepository>();
            services.AddTransient<IVehiclesRepository, VehiclesRepository>();
            services.AddTransient<ISalesRepository, SalesRepository>();
        }
    }
}