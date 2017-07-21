using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Entities;
using VehicleShop.Models.AccountViewModels;

namespace VehicleShop.Controllers
{
    /// <summary>
    /// Provides the APIs for user sign in.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of VehicleShop.Controllers.AccountController.
        /// </summary>
        /// <param name="customersService">The service to use to manage customers.</param>
        /// <param name="userManager">The manager used to manage user accounts e.g. find/create actions.</param>
        /// <param name="signInManager">The manager used to sign in users.</param>
        /// <param name="loggerFactory">The logger used to log messages, warnings and errors.</param>
        public AccountController(
            ICustomersService customersService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _customersService = customersService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        /// <summary>
        /// Returns Login page.
        /// </summary>
        /// <param name="returnUrl">Represents URL to redirect after logging in.</param>
        /// <returns>Login page</returns>
        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Logs in users and redirects to default page or returnUrl.
        /// </summary>
        /// <param name="model">Login data. Includes Email, Password and RememberMe flag.</param>
        /// <param name="returnUrl">Represents URL to redirect after logging in.</param>
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Returns Register page.
        /// </summary>
        /// <param name="returnUrl">Represents URL to redirect after logging in.</param>
        /// <returns>Register page</returns>
        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Registers in users, sign them in and redirects to default page or returnUrl.
        /// </summary>
        /// <param name="model">Register data. Includes Email and Password.</param>
        /// <param name="returnUrl">Represents URL to redirect after logging in.</param>
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _customersService.RegisterCustomerAsync(model.Email, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation(3, "User created a new account with password.");
                    var user = await _userManager.FindByNameAsync(model.Email);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                    return RedirectToLocal(returnUrl);
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Signs the current user out and redirects to Index page.
        /// </summary>
        /// <returns>Index page of VehiclesController.</returns>
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(VehiclesController.Index), "Vehicles");
        }

        /// <summary>
        /// Validates that an email token matches the specific user and redirects to the proper page.
        /// </summary>
        /// <param name="userId">ID of the user who want to confirm his email.</param>
        /// <param name="code">Email confirmation token</param>
        /// <returns></returns>
        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        /// <summary>
        /// Returns Forgot Password page.
        /// </summary>
        /// <returns>Forgot Password page</returns>
        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Generates password reset token and sends to the specific user.
        /// </summary>
        /// <param name="model">ForgotPassword data. Contains Email.</param>
        /// <returns>Redirects to the Forgot Password Confirmation page.</returns>
        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                //return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Returns Forgot Password Confirmation.
        /// </summary>
        /// <returns>Forgot Password Confirmation page</returns>
        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Redirects to Reset Password page.
        /// </summary>
        /// <param name="code">Reset password token</param>
        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        /// <summary>
        /// Validates reset password token and resets the user's password to the specified new password.
        /// </summary>
        /// <param name="model">Reset Password data. Contains email, new password and reset password token.</param>
        /// <returns>Redirects to Reset Password page.</returns>
        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// Returns Reset Password Confirmation page.
        /// </summary>
        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(VehiclesController.Index), "Vehicles");
            }
        }

        #endregion
    }
}