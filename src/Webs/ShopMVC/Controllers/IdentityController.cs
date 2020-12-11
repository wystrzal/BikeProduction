using Common.Application.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Extensions;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models.Dtos;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService identityService;
        private readonly IBus bus;

        public IdentityController(IIdentityService identityService, IBus bus)
        {
            this.identityService = identityService;
            this.bus = bus;
        }

        [ModelErrorsResultFilter(ErrorsName = "LoginErrors")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var tryLogin = await identityService.Login(loginDto);

                if (tryLogin.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Unauthorized.");
            }

            TempData["ModalState"] = "showLogin";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await bus.Publish(new LoggedOutEvent(HttpContext.Session.Id));

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [ModelErrorsResultFilter(ErrorsName = "RegisterErrors")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(registerDto.UserName))
                {
                    ModelState.AddModelError("", "The Password field is required.");
                }

                if (!registerDto.Password.ContainsUpper())
                {
                    ModelState.AddModelError("", "The Password field must have uppercase letters.");
                }

                if (!registerDto.Password.ContainsDigit())
                {
                    ModelState.AddModelError("", "The Password field must have digits.");
                }


                if (ModelState.ErrorCount <= 0)
                {
                    var response = await identityService.Register(registerDto);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
                }
            }

            TempData["ModalState"] = "showRegister";

            return RedirectToAction("Index", "Home");
        }
    }
}