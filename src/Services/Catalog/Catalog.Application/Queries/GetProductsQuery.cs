using Catalog.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<GetProductsDto>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }

        public GetProductsQuery(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}
