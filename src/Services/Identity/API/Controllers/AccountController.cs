using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Extensions;
using Identity.API.Mapping;
using Identity.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            string token = await accountService.TryLogin(loginDto);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(RegisterDto registerDto)
        {
            if (ModelState.IsValid && registerDto.Password.ContainsDigit()
                && registerDto.Password.ContainsUpper())
            {
                if (await accountService.AddUser(registerDto))
                {
                    return Ok();
                }

                return BadRequest("User with this login already exists.");
            }

            return BadRequest("Password must have minimum 6 signs (1 digit, 1 uppercase letter).");
        }

    }
}