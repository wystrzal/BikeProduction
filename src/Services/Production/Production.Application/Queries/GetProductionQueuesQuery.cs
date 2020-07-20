using MediatR;
using Production.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Application.Queries
{
    public class GetProductionQueuesQuery : IRequest<IEnumerable<GetProductionQueuesDto>>
    {
    }
}
