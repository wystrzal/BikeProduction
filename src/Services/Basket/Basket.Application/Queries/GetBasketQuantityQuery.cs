using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Queries
{
    public class GetBasketQuantityQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public GetBasketQuantityQuery(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("UserId");
            }

            UserId = userId;
        }
    }
}
