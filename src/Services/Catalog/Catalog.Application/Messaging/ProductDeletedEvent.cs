using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class ProductDeletedEvent
    {
        public string Reference { get; set; }

        public ProductDeletedEvent(string reference)
        {
            Reference = reference;
        }
    }
}
