using Delivery.Application.Mapping;
using Delivery.Core.SearchSpecification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Queries
{
    public class GetPacksQuery : IRequest<List<GetPacksDto>>
    {
        public FilteringData FilteringData { get; set; }

        public GetPacksQuery(FilteringData filteringData)
        {
            FilteringData = filteringData;
        }
    }
}
