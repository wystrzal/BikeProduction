using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Queries
{
    public class GetBasketQuantityQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public GetBasketQuantityQuery(string userId)
        {
            UserId = userId;
        }
    }
}
