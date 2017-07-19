using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;

namespace VehicleShop.Controllers
{
    [Authorize(Roles = RolesConstants.Customer)]
    public class CustomerController : Controller
    {
        private readonly IVehiclesService _vehiclesService;
        private readonly ICustomersService _customersService;

        public CustomerController(IVehiclesService vehiclesService,
            ICustomersService customersService)
        {
            _vehiclesService = vehiclesService;
            _customersService = customersService;
        }

        public async Task<IActionResult> Index()
        {
            string userName = User.Identity.Name;
            var customer = await _customersService.GetByAppUserNameAsync(userName);
            var vehicles = await _vehiclesService.GetCustomerVehiclesAsync(customer.Id);

            return View(vehicles);
        }
    }
}
