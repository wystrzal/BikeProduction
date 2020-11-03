using Identity.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands
{
    public class TryLoginCommand : IRequest<TokenModel>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
