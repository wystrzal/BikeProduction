using Delivery.Application.Mapping;
using Delivery.Core.SearchSpecification;
using MediatR;
using System.Collections.Generic;

namespace Delivery.Application.Queries
{
    public class GetLoadingPlacesQuery : IRequest<List<GetLoadingPlacesDto>>
    {
        public LoadingPlaceFilteringData FilteringData { get; set; }

        public GetLoadingPlacesQuery(LoadingPlaceFilteringData filteringData)
        {
            FilteringData = filteringData;
        }
    }
}
