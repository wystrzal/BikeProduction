using CustomerOrder.Application.Mapping;
using MediatR;

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
