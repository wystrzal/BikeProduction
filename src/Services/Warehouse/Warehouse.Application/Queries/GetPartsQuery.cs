using MediatR;
using System.Collections.Generic;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.Queries
{
    public class GetPartsQuery : IRequest<IEnumerable<GetPartsDto>>
    {
    }
}
