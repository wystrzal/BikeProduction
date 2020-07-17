using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.Queries
{
    public class GetPartQuery :  IRequest<GetPartDto>
    {
        public int PartId { get; set; }

        public GetPartQuery(int partId)
        {
            PartId = partId;
        }
    }
}
