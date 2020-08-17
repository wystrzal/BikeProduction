using Catalog.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Application.Queries
{
    public class GetHomeProductsQuery : IRequest<List<GetHomeProductsDto>>
    {
        public HomeProduct HomeProduct { get; set; }

        public GetHomeProductsQuery(HomeProduct homeProduct)
        {
            HomeProduct = homeProduct;
        }
    }
}
