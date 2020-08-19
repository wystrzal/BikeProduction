using Catalog.Application.Mapping;
using MediatR;
using System.Collections.Generic;

namespace Catalog.Application.Queries
{
    public class GetBrandsQuery : IRequest<List<GetBrandsDto>>
    {
    }
}
