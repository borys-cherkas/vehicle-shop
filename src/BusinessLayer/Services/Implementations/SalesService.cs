using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.BusinessLayer.Services.Implementations
{
    public class SalesService : ISalesService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVehiclesService _vehiclesService;
        private readonly ICustomersService _customersService;
        private readonly ISalesRepository _salesRepository;

        public SalesService(UserManager<ApplicationUser> userManager,
            IVehiclesService vehiclesService,
            ICustomersService customersService,
            ISalesRepository salesRepository)
        {
            _userManager = userManager;
            _vehiclesService = vehiclesService;
            _customersService = customersService;
            _salesRepository = salesRepository;
        }

        public async Task<bool> CanUserBuyVehicleAsync(string userName, int vehicleId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || !await _userManager.IsInRoleAsync(user, RolesConstants.Customer))
            {
                return false;
            }

            var vehicle = await _vehiclesService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null
                || !vehicle.DistributorId.HasValue
                || vehicle.CustomerId.HasValue)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> BuyVehicleAsync(int vehicleId, string userName)
        {
            if (!await CanUserBuyVehicleAsync(userName, vehicleId))
            {
                throw new Exception("This user cannot buy this vehicle!");
            }

            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            Customer customer = await _customersService.GetByAppUserIdAsync(user.Id);
            Vehicle vehicle = await _vehiclesService.GetVehicleByIdAsync(vehicleId);

            if (vehicle.Cost > customer.Balance)
            {
                return false;
            }

            await _salesRepository.BuyCarAsync(vehicleId, customer.Id);
            return true;
        }
    }
}