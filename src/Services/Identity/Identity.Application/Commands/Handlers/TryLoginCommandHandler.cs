﻿using Identity.Core.Exceptions;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Commands.Handlers
{
    public class TryLoginCommandHandler : IRequestHandler<TryLoginCommand, TokenModel>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;

        public TryLoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        public async Task<TokenModel> Handle(TryLoginCommand request, CancellationToken cancellationToken)
        {
            User dbUser = await GetUser(request);

            return await GenerateTokenIfLoginSuccessful(request, dbUser);
        }

        private async Task<User> GetUser(TryLoginCommand request)
        {
            var dbUser = await userManager.FindByNameAsync(request.Username);

            if (dbUser == null)
            {
                throw new UserNotFoundException();
            }

            return dbUser;
        }

        private async Task<TokenModel> GenerateTokenIfLoginSuccessful(TryLoginCommand request, User dbUser)
        {
            var result = await signInManager.CheckPasswordSignInAsync(dbUser, request.Password, false);

            if (result.Succeeded)
            {
                return await tokenService.GenerateToken(dbUser, userManager);
            }

            throw new LoginFailedException();
        }
    }
}
