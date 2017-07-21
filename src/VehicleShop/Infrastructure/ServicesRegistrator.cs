using Microsoft.Extensions.DependencyInjection;

namespace VehicleShop.Infrastructure
{
    public static class ServicesRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            BusinessLayer.Infrastructure.ServicesRegistrator.Register(services);
        }
    }
}