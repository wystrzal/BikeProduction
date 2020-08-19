using Identity.Core.Models;
using MediatR;

namespace Identity.Application.Commands
{
    public class TryLoginCommand : IRequest<TokenModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
