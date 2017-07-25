using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Extensions;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.DataLayer.Repositories.Implementations
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Vehicle> _vehicles;

        public VehiclesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _vehicles = dbContext.Vehicles;
        }

        public async Task<IList<Vehicle>> GetVehiclesAsync(
            Func<IQueryable<Vehicle>, IQueryable<Vehicle>> queryFunc = null)
        {
            var query = _vehicles
                .ProcessQuery(queryFunc);

            return await query.ToListAsync();
        }

        public Task<Vehicle> GetVehicleAsync(int vehicleId,
            Func<IQueryable<Vehicle>, IQueryable<Vehicle>> queryFunc = null)
        {
            var query = _vehicles
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.Id == vehicleId);
        }

        public async Task CreateAsync(Vehicle vehicleModel)
        {
            await _dbContext.Vehicles.AddAsync(vehicleModel);
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateVehicleAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            return _dbContext.SaveChangesAsync();
        }
    }
}