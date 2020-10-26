using CustomerOrder.Application.Mapping;
using MediatR;
using System;

namespace CustomerOrder.Application.Queries
{
    public class GetOrderQuery : IRequest<GetOrderDto>
    {
        public int OrderId { get; set; }

        public GetOrderQuery(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("OrderId must be greater than zero.");
            }

            OrderId = orderId;
        }
    }
}
