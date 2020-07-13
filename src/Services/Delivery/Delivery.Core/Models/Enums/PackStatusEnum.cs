using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.Models.Enums
{
    public class PackStatusEnum
    {
        public enum PackStatus
        {
            Waiting = 1,
            ReadyToSend = 2,
            Sended = 3
        }
    }
}
