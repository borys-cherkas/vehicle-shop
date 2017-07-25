using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;
using VehicleShop.Models.Distributor;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.Controllers
{
    [Authorize(Roles = RolesConstants.Distributor)]
    public class DistributorsController : Controller
    {
        private readonly IDistributorsService _distributorsService;
        private readonly IVehiclesService _vehiclesService;
        private readonly ITransactionsService _transactionsService;

        public DistributorsController(IDistributorsService distributorsService,
            IVehiclesService vehiclesService,
            ITransactionsService transactionsService)
        {
            _distributorsService = distributorsService;
            _vehiclesService = vehiclesService;
            _transactionsService = transactionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DistributorsMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == DistributorsMessageId.Error ? "An error has occurred."
                : message == DistributorsMessageId.VehicleSalesStateChangedSuccess ? "Vehicle sales state has changed successfully."
                : message == DistributorsMessageId.UpdateVehicleSuccess ? "Vehicle has been updated successfully."
                : message == DistributorsMessageId.CreateVehicleSuccess ? "Vehicle has been created successfully."
                : "";

            string username = User.Identity.Name;
            var distributor = await _distributorsService.GetDistributorByUserNameAsync(username);
            var vehicles = await _vehiclesService.GetDistributorVehiclesAsync(distributor.Id);
            var transactions = await _transactionsService.GetTransactionsByDistributorIdAsync(distributor.Id);

            var vm = new IndexViewModel
            {
                Distributor = distributor,
                Vehicles = vehicles,
                Transactions = transactions
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeVehicleSalesState(int vehicleId, bool newState)
        {
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(vehicleId);
            var vm = new ChangeVehicleSalesStateViewModel
            {
                VehicleId = vehicleId,
                Vehicle = vehicle,
                NewSalesState = newState
            };
            return View("ChangeVehicleSalesStateConfirmation", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeVehicleSalesState(ChangeVehicleSalesStateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangeVehicleSalesStateConfirmation", model);
            }

            await _vehiclesService.ChangeSalesStateAsync(model.VehicleId, model.NewSalesState);

            return RedirectToAction(nameof(Index),
                new { Message = DistributorsMessageId.VehicleSalesStateChangedSuccess });
        }

        [HttpGet]
        public IActionResult CreateVehicle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(EditVehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = User.Identity.Name;
            var distributor = await _distributorsService.GetDistributorByUserNameAsync(userName);

            var vehicleModel = new Vehicle()
            {
                IsSelling = model.IsSelling,
                Cost = model.Cost,
                Description = model.Description,
                Name = model.Name,
                DistributorId = distributor.Id
            };
            await _vehiclesService.CreateVehicleAsync(vehicleModel);

            return RedirectToAction("Index", new { Message = DistributorsMessageId.CreateVehicleSuccess });
        }

        [HttpGet]
        public async Task<IActionResult> EditVehicle(int vehicleId)
        {
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(vehicleId);
            var vm = new EditVehicleViewModel(vehicle);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditVehicle(EditVehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var vehicle = await _vehiclesService.GetVehicleByIdAsync(model.Id);

            vehicle.Name = model.Name;
            vehicle.Description = model.Description;
            vehicle.Cost = model.Cost;
            vehicle.IsSelling = model.IsSelling;

            await _vehiclesService.UpdateAsync(vehicle);

            return RedirectToAction("Index", new { Message = DistributorsMessageId.UpdateVehicleSuccess });
        }


        public enum DistributorsMessageId
        {
            VehicleSalesStateChangedSuccess,
            CreateVehicleSuccess,
            UpdateVehicleSuccess,
            Error
        }
    }
}