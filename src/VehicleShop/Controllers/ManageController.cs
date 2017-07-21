using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleShop.DataLayer.Entities;
using VehicleShop.Models;
using VehicleShop.Models.ManageViewModels;

namespace VehicleShop.Controllers
{
    /// <summary>
    /// Provides the abilities to manage the user's account info.
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of VehicleShop.Controllers.ManageController.
        /// </summary>
        /// <param name="userManager">Provides the APIs for managing user.</param>
        /// <param name="signInManager">Represents service for user sign in functionality.</param>
        /// <param name="loggerFactory">Represents a type used to configure the logging system 
        /// and create instances of Microsoft.Extensions.Logging.ILogger.</param>
        public ManageController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        /// <summary>
        /// Returns Index page.
        /// </summary>
        /// <param name="message">Status message</param>
        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.Error
                    ? "An error has occurred."
                    : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View();
        }

        /// <summary>
        /// Returns Change Password page.
        /// </summary>
        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes a user's password, sings user in and redirects to Manage/Index page.
        /// </summary>
        /// <param name="model">Change Password data. Contains old and new passwords.</param>
        /// <returns>Index page with status message.</returns>
        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new {Message = ManageMessageId.ChangePasswordSuccess});
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new {Message = ManageMessageId.Error});
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}