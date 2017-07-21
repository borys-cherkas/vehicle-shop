using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;
using VehicleShop.Models.Customer;
using VehicleShop.Models.ManageViewModels;

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
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] = 
                message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.ReplenishBalanceSuccess ? "Balance has been replenished successfully"
                : "";

            string userName = User.Identity.Name;
            var customer = await _customersService.GetByAppUserNameAsync(userName);
            var vehicles = await _vehiclesService.GetCustomerVehiclesAsync(customer.Id);

            var vm = new IndexViewModel()
            {
                Vehicles = vehicles,
                Customer = customer
            };

            return View(vm);
        }


        [HttpGet]
        [Authorize(Roles = RolesConstants.Customer)]
        public IActionResult ReplenishBalance()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Customer)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplenishBalance(ReplenishAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = await _customersService.GetByAppUserNameAsync(User.Identity.Name);
            await _customersService.ReplenishBalanceAsync(customer.Id, model.Amount);

            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ReplenishBalanceSuccess });
        }



        public enum ManageMessageId
        {
            ReplenishBalanceSuccess,
            Error
        }
    }
}