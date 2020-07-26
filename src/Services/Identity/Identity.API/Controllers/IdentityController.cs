using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Application.Extensions;
using Identity.Application.Commands;
using Identity.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator mediator;

        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(TryLoginCommand command)
        {
            try
            {
                return Ok(await mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterCommand command)
        {
            try
            {
                if (ModelState.IsValid && command.Password.ContainsDigit()
                    && command.Password.ContainsUpper())
                {
                    return Ok(await mediator.Send(command));
                }

                return BadRequest("Password must have minimum 6 signs (1 digit, 1 uppercase letter).");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}