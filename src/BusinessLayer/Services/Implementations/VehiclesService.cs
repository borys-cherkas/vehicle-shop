using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.BusinessLayer.Services.Implementations
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        public VehiclesService(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        public Task<IList<Vehicle>> GetVehiclesForSaleAsync()
        {
            return _vehiclesRepository.GetVehiclesAsync(query => query
                .Where(v => v.DistributorId.HasValue)
                .Include(x => x.Distributor));
        }

        public Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            return _vehiclesRepository.GetVehicleAsync(vehicleId);
        }

        public async Task UpdateVehicleWithCustomerAsync(int vehicleId, int customerId)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            vehicle.DistributorId = null;
            vehicle.CustomerId = customerId;

            await _vehiclesRepository.UpdateVehicleAsync(vehicle);
        }

        public Task<IList<Vehicle>> GetCustomerVehiclesAsync(int customerId)
        {
            return _vehiclesRepository.GetVehiclesAsync(q => q
                .Where(v => v.CustomerId == customerId));
        }
    }
}