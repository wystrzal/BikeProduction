using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.Queries
{
    public class GetPartsQuery : IRequest<IEnumerable<GetPartsDto>>
    {
    }
}
