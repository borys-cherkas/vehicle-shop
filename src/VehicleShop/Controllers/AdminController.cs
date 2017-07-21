using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;

namespace VehicleShop.Controllers
{
    /// <summary>
    /// Represents abilities for administrators to manage distributors and customers.
    /// </summary>
    [Authorize(Roles = RolesConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IDistributorsService _distributorsService;

        /// <summary>
        /// Creates a new instance of VehicleShop.Controllers.AdminController.
        /// </summary>
        /// <param name="distributorsService">The service to manage distributors.</param>
        public AdminController(IDistributorsService distributorsService)
        {
            _distributorsService = distributorsService;
        }

        /// <summary>
        /// Returns Index page with distributors.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var distributors = await _distributorsService.GetDistributorsAsync();
            return View(distributors);
        }
    }
}
