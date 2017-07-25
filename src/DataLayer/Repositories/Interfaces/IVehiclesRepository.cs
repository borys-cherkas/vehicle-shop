using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Repositories.Interfaces
{
    public interface IVehiclesRepository
    {
        Task<IList<Vehicle>> GetVehiclesAsync(Func<IQueryable<Vehicle>, IQueryable<Vehicle>> queryFunc = null);

        Task<Vehicle> GetVehicleAsync(int vehicleId, Func<IQueryable<Vehicle>, IQueryable<Vehicle>> queryFunc = null);

        Task CreateAsync(Vehicle vehicleModel);

        Task UpdateVehicleAsync(Vehicle vehicle);
    }
}
