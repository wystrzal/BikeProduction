﻿using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Handlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
    {
        private readonly IBasketRedisService basketRedisService;

        public RemoveProductCommandHandler(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRedisService.GetBasket(request.UserId);

            if (basket.Products.Count > 0)
            {
                await RemoveProductAndDecreaseBasketTotalPrice(basket, request);
            }

            return Unit.Value;
        }

        private async Task RemoveProductAndDecreaseBasketTotalPrice(UserBasketDto basket, RemoveProductCommand request)
        {
            var productToRemove = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

            if (productToRemove == null)
            {
                return;
            }

            basket.Products.Remove(productToRemove);
            basket.TotalPrice -= (productToRemove.Price * productToRemove.Quantity);

            await basketRedisService.SaveBasket(request.UserId, basket);
        }
    }
}

