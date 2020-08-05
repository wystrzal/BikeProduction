using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Queries.Handlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, UserBasketDto>
    {
        private readonly IBasketRedisService basketService;

        public GetBasketQueryHandler(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<UserBasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return await basketService.GetBasket(request.UserId);
        }
    }
}
