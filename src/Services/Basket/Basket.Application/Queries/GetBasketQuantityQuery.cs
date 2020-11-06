using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Queries
{
    public class GetBasketQuantityQuery : UserIdQuery<int>
    {
        public GetBasketQuantityQuery(string userId) : base(userId)
        {
        }
    }
}
