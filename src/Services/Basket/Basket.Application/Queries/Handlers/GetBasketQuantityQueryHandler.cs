using Basket.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Queries.Handlers
{
    public class GetBasketQuantityQueryHandler : IRequestHandler<GetBasketQuantityQuery, int>
    {
        private readonly IBasketRedisService basketService;

        public GetBasketQuantityQueryHandler(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<int> Handle(GetBasketQuantityQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasket(request.UserId);

            return basket == null ? 0 : basket.Products.Count;
        }
    }
}
