using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyManagementMVC.Interfaces;
using CompanyManagementMVC.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementMVC.Controllers
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
                    return RedirectToAction("Index", "Home");           
            }

            ModelState.AddModelError("", "Unauthorized.");
            return View("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}