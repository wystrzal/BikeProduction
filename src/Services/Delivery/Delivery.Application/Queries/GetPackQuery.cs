using Delivery.Application.Mapping;
using MediatR;
using System;

namespace Delivery.Application.Queries
{
    public class GetPackQuery : IRequest<GetPackDto>
    {
        public int PackId { get; set; }

        public GetPackQuery(int packId)
        {
            if (packId <= 0)
            {
                throw new ArgumentException("PackId must be greater than zero.");
            }

            PackId = packId;
        }
    }
}
