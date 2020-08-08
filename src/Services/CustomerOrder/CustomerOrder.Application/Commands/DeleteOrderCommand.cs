using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
