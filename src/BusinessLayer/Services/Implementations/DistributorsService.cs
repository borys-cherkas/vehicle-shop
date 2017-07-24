using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Distributor> GetDistributorByIdAsync(int id)
        {
            return _distributorsRepository.GetDistributorByIdAsync(id,
                x => x.Include(d => d.User));
        }

        public Task<Distributor> GetDistributorByUserIdAsync(string userId)
        {
            return _distributorsRepository.GetDistributorByUserIdAsync(userId);
        }

        public Task<Distributor> GetDistributorByUserNameAsync(string username)
        {
            return _distributorsRepository.GetDistributorByAppUserNameAsync(username);
        }

        public Task UpdateDistributorAsync(Distributor distributor)
        {
            return _distributorsRepository.UpdateAsync(distributor);
        }
    }
}