using MediatR;
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

namespace Basket.Application.Commands
{
    public class ChangeProductQuantityCommand : IRequest
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public ChangeQuantityAction ChangeQuantityAction { get; set; }
    }
}
