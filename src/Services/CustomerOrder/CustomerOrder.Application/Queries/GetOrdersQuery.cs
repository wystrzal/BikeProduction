using CustomerOrder.Application.Mapping;
using MediatR;
using System.Collections.Generic;

namespace CustomerOrder.Application.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<GetOrdersDto>>
    {
        public string UserId { get; set; }

        public GetOrdersQuery(string userId)
        {
            UserId = userId;
        }
    }
}
