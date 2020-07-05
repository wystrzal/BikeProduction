using AutoMapper;
using Identity.Application.Commands;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, 
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task<string> TryLogin(TryLoginCommand command)
        {
            var dbUser = await userManager.FindByNameAsync(command.Username);

            if (dbUser == null)
            {
                throw new Exception("Not found.");
            }

            var result = await signInManager.CheckPasswordSignInAsync(dbUser, command.Password, false);

            if (result.Succeeded)
            {
                return await tokenService.GenerateToken(dbUser, userManager);
            }

            throw new Exception("Failed to login");
        }

        public async Task AddUser(AddUserCommand command)
        {
            var userToCreate = mapper.Map<User>(command);

            var result = await userManager.CreateAsync(userToCreate, command.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User with this login already exist.");
            }
        }
    }
}
