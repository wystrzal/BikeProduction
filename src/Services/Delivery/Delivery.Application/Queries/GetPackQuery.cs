using Delivery.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Queries
{
    public class GetPackQuery : IRequest<GetPackDto>
    {
        public int PackId { get; set; }

        public GetPackQuery(int packId)
        {
            PackId = packId;
        }
    }
}
