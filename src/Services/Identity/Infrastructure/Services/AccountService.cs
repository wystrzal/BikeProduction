using AutoMapper;
using Identity.API.Mapping;
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

        public async Task<string> TryLogin(LoginDto loginDto)
        {
            var dbUser = await userManager.FindByNameAsync(loginDto.Username);

            if (dbUser == null)
            {
                return null;
            }

            var result = await signInManager.CheckPasswordSignInAsync(dbUser, loginDto.Password, false);

            if (result.Succeeded)
            {
                return await tokenService.GenerateToken(dbUser, userManager);
            }

            return null;
        }

        public async Task<bool> AddUser(RegisterDto registerDto)
        {
            var userToCreate = mapper.Map<User>(registerDto);

            var result = await userManager.CreateAsync(userToCreate, registerDto.Password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
