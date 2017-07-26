using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Constants;
using VehicleShop.DataLayer.Entities;
using VehicleShop.Models.Admin;
using VehicleShop.BusinessLayer.Models;
using VehicleShop.Extensions;

namespace VehicleShop.Controllers
{
    /// <summary>
    /// Represents abilities for administrators to manage distributors and customers.
    /// </summary>
    [Authorize(Roles = RolesConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IDistributorsService _distributorsService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Creates a new instance of VehicleShop.Controllers.AdminController.
        /// </summary>
        /// <param name="distributorsService">The service to manage distributors.</param>
        public AdminController(IDistributorsService distributorsService,
            UserManager<ApplicationUser> userManager)
        {
            _distributorsService = distributorsService;
            _userManager = userManager;
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

        [HttpGet]
        public IActionResult CreateDistributor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDistributor(CreateDistributorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CreateDistributorDTO dto = model.MapToCreateDistributorDTO();

            var res = await _distributorsService.CreateDistributorWithUserAsync(dto);
            if (!res.Succeeded)
            {
                AddErrors(res);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns Edit Distributor Credentials page.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> EditDistributorCredentials(int distributorId)
        {
            var distributor = await _distributorsService.GetDistributorByIdAsync(distributorId);
            var editCredentialsViewModel = new EditDistributorCredentialsViewModel()
            {
                Email = distributor.User.Email,
                UserId = distributor.UserId
            };
            return View(editCredentialsViewModel);
        }

        /// <summary>
        /// Allow administrators to edit distributor's credentials.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> EditDistributorCredentials(EditDistributorCredentialsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser userByEmail = await _userManager.FindByEmailAsync(model.Email);
            bool emailAlreadyExists = userByEmail != null && userByEmail.Id != model.UserId;
            if (emailAlreadyExists)
            {
                ModelState.AddModelError("Email", "User with the same email already exists.");
                return View(model);
            }


            ApplicationUser user = await _userManager.FindByIdAsync(model.UserId);

            user.UserName = model.Email;
            user.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Password))
            {
                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        #region Helpers 

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}