using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.BusinessLayer.Services.Implementations
{
    public class DistributorsService : IDistributorsService
    {
        private readonly IDistributorsRepository _distributorsRepository;

        public DistributorsService(IDistributorsRepository distributorsRepository)
        {
            _distributorsRepository = distributorsRepository;
        }

        public Task<IList<Distributor>> GetDistributorsAsync()
        {
            return _distributorsRepository.GetDistributorsAsync(q => q
                .Include(d => d.Vehicles)
                .Include(d => d.User));
        }
    }
}