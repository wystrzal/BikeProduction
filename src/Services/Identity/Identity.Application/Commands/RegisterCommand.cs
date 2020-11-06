using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands
{
    public class RegisterCommand : BaseCommand, IRequest
    {
    }
}
