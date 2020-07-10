using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class ConfirmProductionEvent
    {
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}
