using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands
{
    public class RegisterCommand : IRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
