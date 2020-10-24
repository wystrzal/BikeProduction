using MediatR;
using Production.Application.Mapping;
using Production.Core.Models;
using System.Collections.Generic;

namespace Production.Application.Queries
{
    public class GetProductionQueuesQuery : IRequest<IEnumerable<GetProductionQueuesDto>>
    {
        public ProductionQueueFilteringData FilteringData { get; set; }

        public GetProductionQueuesQuery(ProductionQueueFilteringData filteringData)
        {
            FilteringData = filteringData;
        }
    }
}
