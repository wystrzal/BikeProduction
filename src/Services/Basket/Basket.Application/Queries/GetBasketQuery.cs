using Basket.Core.Dtos;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketQuery : IRequest<UserBasketDto>
    {
        public string UserId { get; set; }

        public GetBasketQuery(string userId)
        {
            UserId = userId;
        }
    }
}
