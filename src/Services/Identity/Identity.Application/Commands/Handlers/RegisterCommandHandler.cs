﻿using AutoMapper;
using Identity.Core.Exceptions;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Commands.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public RegisterCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = mapper.Map<User>(request);

            await CheckIfUserAlreadyExist(userToCreate);

            await CreateUserAccount(request, userToCreate);

            return Unit.Value;
        }

        private async Task CheckIfUserAlreadyExist(User userToCreate)
        {
            var user = await userManager.FindByNameAsync(userToCreate.UserName);

            if (user != null)
            {
                throw new UserAlreadyExistException(user.UserName);
            }
        }

        private async Task CreateUserAccount(RegisterCommand request, User userToCreate)
        {
            var result = await userManager.CreateAsync(userToCreate, request.Password);

            if (!result.Succeeded)
            {
                throw new UserNotAddedException();
            }
        }
    }
}
