using Delivery.Application.Mapping;
using Delivery.Core.SearchSpecification;
using MediatR;
using System.Collections.Generic;

namespace Delivery.Application.Queries
{
    public class GetPacksQuery : IRequest<List<GetPacksDto>>
    {
        public OrderFilteringData FilteringData { get; set; }

        public GetPacksQuery(OrderFilteringData filteringData)
        {
            FilteringData = filteringData;
        }
    }
}
