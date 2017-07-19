using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;

namespace VehicleShop.Controllers
{
    [Authorize(Roles = RolesConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IDistributorsService _distributorsService;

        public AdminController(IDistributorsService distributorsService)
        {
            _distributorsService = distributorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var distributors = await _distributorsService.GetDistributorsAsync();
            return View(distributors);
        }
    }
}
