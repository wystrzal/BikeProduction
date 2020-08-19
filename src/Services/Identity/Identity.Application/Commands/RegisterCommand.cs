using MediatR;

namespace Identity.Application.Commands
{
    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
