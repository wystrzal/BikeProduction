using MediatR;
using System;
using System.Collections.Generic;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.Queries
{
    public class GetProductPartsQuery : IRequest<List<GetProductPartsDto>>
    {
        public string Reference { get; set; }

        public GetProductPartsQuery(string reference)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                throw new ArgumentNullException("Reference");
            }

            Reference = reference;
        }
    }
}
