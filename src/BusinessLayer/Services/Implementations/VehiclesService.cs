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
                .Where(v => v.DistributorId.HasValue && v.IsSelling)
                .Include(x => x.Distributor));
        }

        public Task<IList<Vehicle>> GetDistributorVehiclesAsync(int distributorId)
        {
            return _vehiclesRepository.GetVehiclesAsync(query => query
                .Where(v => v.DistributorId == distributorId));
        }

        public Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            return _vehiclesRepository.GetVehicleAsync(vehicleId);
        }

        public Task<IList<Vehicle>> GetCustomerVehiclesAsync(int customerId)
        {
            return _vehiclesRepository.GetVehiclesAsync(q => q
                .Where(v => v.CustomerId == customerId));
        }

        public async Task ChangeSalesStateAsync(int vehicleId, bool newSalesState)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            vehicle.IsSelling = newSalesState;
            await _vehiclesRepository.UpdateVehicleAsync(vehicle);
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            return _vehiclesRepository.UpdateVehicleAsync(vehicle);
        }
    }
}