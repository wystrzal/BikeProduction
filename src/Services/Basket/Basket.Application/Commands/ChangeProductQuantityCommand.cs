using MediatR;
using System.ComponentModel.DataAnnotations;
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

namespace Basket.Application.Commands
{
    public class ChangeProductQuantityCommand : IRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ChangeQuantityAction ChangeQuantityAction { get; set; }
    }
}
