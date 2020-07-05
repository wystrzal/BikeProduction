using Identity.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Commands.Handlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IAccountService accountService;

        public AddUserCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await accountService.AddUser(request);

            return Unit.Value;
        }
    }
}
