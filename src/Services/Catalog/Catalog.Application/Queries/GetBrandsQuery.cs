using Catalog.Application.Mapping;
using Catalog.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetBrandsQuery : IRequest<List<GetBrandsDto>>
    {
    }
}
