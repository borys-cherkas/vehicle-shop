using Microsoft.Extensions.DependencyInjection;

namespace VehicleShop.BusinessLayer.Infrastructure
{
    public static class ServicesRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            DataLayer.Infrastructure.ServicesRegistrator.Register(services);
        }
    }
}
