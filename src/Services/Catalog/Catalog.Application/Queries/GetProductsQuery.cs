using Catalog.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<GetProductsDto>>
    {
    }
}
