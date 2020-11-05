using MediatR;
using System;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.Queries
{
    public class GetPartQuery : IRequest<GetPartDto>
    {
        public int PartId { get; set; }

        public GetPartQuery(int partId)
        {
            if (partId <= 0)
            {
                throw new ArgumentException("PartId must be greater than zero.");
            }

            PartId = partId;
        }
    }
}
