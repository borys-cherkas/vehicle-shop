using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;

namespace VehicleShop.Controllers
{
    /// <summary>
    /// Represents abilities for customers to manage vehicles and transactions.
    /// </summary>
    [Authorize(Roles = RolesConstants.Customer)]
    public class CustomerController : Controller
    {
        private readonly IVehiclesService _vehiclesService;
        private readonly ICustomersService _customersService;

        /// <summary>
        /// Creates a new instance of VehicleShop.Controllers.CustomerController.
        /// </summary>
        /// <param name="vehiclesService">Represents service to manage vehicles.</param>
        /// <param name="customersService">Represents service to manage customers.</param>
        public CustomerController(IVehiclesService vehiclesService,
            ICustomersService customersService)
        {
            _vehiclesService = vehiclesService;
            _customersService = customersService;
        }

        /// <summary>
        /// Returns Index page with vehicles.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            string userName = User.Identity.Name;
            var customer = await _customersService.GetByAppUserNameAsync(userName);
            var vehicles = await _vehiclesService.GetCustomerVehiclesAsync(customer.Id);

            return View(vehicles);
        }
    }
}
