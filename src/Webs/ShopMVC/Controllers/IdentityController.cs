using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMVC.Extensions;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models.Dtos;

namespace ShopMVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
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

                var response = await identityService.Register(registerDto);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
            }

            TempData["ModalState"] = "showRegister";

            return RedirectToAction("Index", "Home");
        }
    }
}