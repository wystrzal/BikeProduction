using Basket.Core.Dtos;

namespace Basket.Application.Queries
{
    public class GetBasketQuery : UserIdQuery<UserBasketDto>
    {
        public GetBasketQuery(string userId) : base(userId)
        {
        }
    }
}
