using MediatR;
using Production.Application.Mapping;
using System.Collections.Generic;

namespace Production.Application.Queries
{
    public class GetProductionQueuesQuery : IRequest<IEnumerable<GetProductionQueuesDto>>
    {
    }
}
