using Catalog.Application.Mapping;
using Catalog.Core.SearchSpecification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<GetProductsDto>>
    {
        public FilteringData FilteringData { get; set; }

        public GetProductsQuery(FilteringData filteringData)
        {
            FilteringData = filteringData;
        }
    }
}
