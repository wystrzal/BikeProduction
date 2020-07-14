using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Application.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public int OrderId { get; set; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
