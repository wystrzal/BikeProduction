using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class ClearBasketCommand : IRequest
    {
        [Required]
        public string UserId { get; set; }

        public ClearBasketCommand(string userId)
        {
            UserId = userId;
        }
    }
}
