﻿using Catalog.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetProductQuery : IRequest<GetProductDto>
    {
        public int ProductId { get; set; }

        public GetProductQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
