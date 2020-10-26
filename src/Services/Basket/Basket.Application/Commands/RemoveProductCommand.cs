using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class RemoveProductCommand : IRequest
    {
        [Required]
        public string UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        public RemoveProductCommand(string userId, int productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
