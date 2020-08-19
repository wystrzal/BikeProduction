using MediatR;

namespace CustomerOrder.Application.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }

        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
