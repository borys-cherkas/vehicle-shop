using Microsoft.Extensions.DependencyInjection;
using VehicleShop.BusinessLayer.Services.Implementations;
using VehicleShop.BusinessLayer.Services.Interfaces;

namespace VehicleShop.BusinessLayer.Infrastructure
{
    public static class ServicesRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            DataLayer.Infrastructure.ServicesRegistrator.Register(services);

            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<IVehiclesService, VehiclesService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IDistributorsService, DistributorsService>();
        }
    }
}
