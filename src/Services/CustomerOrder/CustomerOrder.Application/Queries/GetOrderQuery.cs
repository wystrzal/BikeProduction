using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Application.Queries
{
    public class GetOrderQuery : IRequest<GetOrderDto>
    {
        public int OrderId { get; set; }

        public GetOrderQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
