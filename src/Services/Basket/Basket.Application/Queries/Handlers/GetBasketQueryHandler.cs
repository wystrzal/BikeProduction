using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Queries.Handlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, UserBasketDto>
    {
        private readonly IBasketRedisService basketRedisService;

        public GetBasketQueryHandler(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }

        public async Task<UserBasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return await basketRedisService.GetBasket(request.UserId);
        }
    }
}
