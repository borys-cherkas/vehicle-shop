using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;
using VehicleShop.DataLayer.Entities;
using VehicleShop.Models.VehicleViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleShop.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehiclesService _vehiclesService;
        private readonly ICustomersService _customersService;
        private readonly ISalesService _salesService;
        private readonly ILogger _logger;

        public VehiclesController(
            IVehiclesService vehiclesService,
            ICustomersService customersService,
            ISalesService salesService,
            ILoggerFactory loggerFactory)
        {
            _vehiclesService = vehiclesService;
            _customersService = customersService;
            _salesService = salesService;
            _logger = loggerFactory.CreateLogger(typeof(VehiclesController));
        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehiclesForSales = await _vehiclesService.GetVehiclesForSaleAsync();
            return View(vehiclesForSales);
        }
        
        [Authorize(Roles = RolesConstants.Customer)]
        public async Task<IActionResult> BuyVehicle(int vehicleId)
        {
            string userName = User.Identity.Name;
            bool isUserAllowed = await _salesService.CanUserBuyVehicleAsync(userName, vehicleId);
            if (!isUserAllowed)
            {
                _logger.LogWarning(5, "Unauthorized attempt to buy vehicle!");
                return Unauthorized();
            }

            Vehicle vehicle = await _vehiclesService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                _logger.LogWarning(5, "Vehicle not found!");
                return View("Error");
            }

            return View(vehicle);
        }

        [Authorize(Roles = RolesConstants.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyVehicle(BuyVehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(4, "Invalid attempt to buy vehicle! Model is not valid.");
                return View("Error");
            }

            string userName = User.Identity.Name;
            bool isUserAllowed = await _salesService.CanUserBuyVehicleAsync(userName, model.Id);
            if (!isUserAllowed)
            {
                _logger.LogWarning(5, "Unauthorized attempt to buy vehicle!");
                return Unauthorized();
            }
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(model.Id);
            var customer = await _customersService.GetByAppUserNameAsync(userName);
            if (customer.Balance < vehicle.Cost)
            {
                return View("LowBalanse");
            }

            await _salesService.BuyVehicleAsync(model.Id, userName);

            return View("BuyVehicleSuccessful");
        }
    }
}
