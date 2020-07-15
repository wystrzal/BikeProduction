using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Application.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<GetOrdersDto>>
    {
        public string UserId { get; set; }

        public GetOrdersQuery(string userId)
        {
            UserId = userId;
        }
    }
}
