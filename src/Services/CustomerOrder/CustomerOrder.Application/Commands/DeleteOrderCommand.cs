using MediatR;
using System;

namespace CustomerOrder.Application.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }

        public DeleteOrderCommand(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("OrderId must be greater than zero.");
            }

            OrderId = orderId;
        }
    }
}
