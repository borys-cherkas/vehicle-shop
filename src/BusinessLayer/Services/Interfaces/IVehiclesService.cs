using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.BusinessLayer.Services.Interfaces
{
    public interface IVehiclesService
    {
        Task<IList<Vehicle>> GetVehiclesForSaleAsync();

        Task<IList<Vehicle>> GetDistributorVehiclesAsync(int distributorId);

        Task<IList<Vehicle>> GetCustomerVehiclesAsync(int customerId);

        Task<Vehicle> GetVehicleByIdAsync(int vehicleId);

        Task ChangeSalesStateAsync(int vehicleId, bool newSalesState);

        Task UpdateAsync(Vehicle vehicle);
    }
}
