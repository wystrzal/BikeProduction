using AutoMapper;
using Identity.Core.Exceptions;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Commands.Handlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public AddUserCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = mapper.Map<User>(request);

            var user = await userManager.FindByNameAsync(userToCreate.UserName);

            if (user != null)
            {
                throw new UserAlreadyExistException(user.UserName);
            }

            var result = await userManager.CreateAsync(userToCreate, request.Password);

            if (!result.Succeeded)
            {
                throw new UserNotAddedException();
            }

            return Unit.Value;
        }
    }
}
