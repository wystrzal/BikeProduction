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
    public class TryLoginCommandHandler : IRequestHandler<TryLoginCommand, string>
    {
        private readonly IAccountService accountService;

        public TryLoginCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public async Task<string> Handle(TryLoginCommand request, CancellationToken cancellationToken)
        {
            return await accountService.TryLogin(request);
        }
    }
}
