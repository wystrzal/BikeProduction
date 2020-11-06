using Basket.Core.Dtos;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Queries
{
    public class GetBasketQuery : BaseQuery<UserBasketDto>
    {
        public GetBasketQuery(string userId) : base(userId)
        {
        }
    }
}
