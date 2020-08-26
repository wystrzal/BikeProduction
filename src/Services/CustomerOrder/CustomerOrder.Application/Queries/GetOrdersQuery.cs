using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.SearchSpecification;
using MediatR;
using System.Collections.Generic;

namespace CustomerOrder.Application.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<GetOrdersDto>>
    {
        public FilteringData FilteringData { get; set; }

        public GetOrdersQuery(FilteringData filteringData)
        {
            FilteringData = filteringData;
        }
    }
}
