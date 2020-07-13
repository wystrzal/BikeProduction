using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Commands
{
    public class PackReadyToSendEvent
    {
        public int OrderId { get; set; }

        public PackReadyToSendEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
