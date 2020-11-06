using Identity.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands
{
    public class TryLoginCommand : BaseCommand, IRequest<TokenModel>
    {
        [Required]
        public string SessionId { get; set; }
    }
}
