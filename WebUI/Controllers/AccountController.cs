using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WebUI.Localization;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User loged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return Content($"confirmation required ({model.Email})");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return PartialView("_RegistrationSuccessPartial");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.ResourceManager.GetString(error.Code));
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return PartialView("_LoginSuccessPartial");
                }
                if (result.RequiresTwoFactor)
                {
                    return Content("2-Factor login not configured");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return Content("This account is locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.invalidLoginAttempt);
                    //return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView("Login");
        }


        [HttpGet]
        public IActionResult ReloadNavi()
        {
            return PartialView("~/Views/Shared/_NavActionsPartial.cshtml");
        }
    }
}