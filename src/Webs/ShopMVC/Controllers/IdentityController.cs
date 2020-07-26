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

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var tryLogin = await identityService.Login(loginDto);

                if (tryLogin.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var token = await tryLogin.Content.ReadAsStringAsync();

                    AuthenticationProperties options = new AuthenticationProperties();

                    options.AllowRefresh = true;
                    options.IsPersistent = true;
                    options.ExpiresUtc = DateTime.Now.AddDays(1);

                    var claims = new List<Claim>
                    {
                       new Claim("AcessToken", token)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        options);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Unauthorized.");
            }
      
            TempData["ModalState"] = "show";

            if (TempData["ModelErrors"] == null)
            {
                TempData.Add("ModelErrors", new List<string>());
            }

            foreach (var obj in ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        ((List<string>)TempData["ModelErrors"]).Add(error.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

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

            TempData["ModalState"] = "show";

            if (TempData["ModelErrors"] == null)
            {
                TempData.Add("ModelErrors", new List<string>());
            }

            foreach (var obj in ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        ((List<string>)TempData["ModelErrors"]).Add(error.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}