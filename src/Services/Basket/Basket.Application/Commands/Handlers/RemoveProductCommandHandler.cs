﻿using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using Newtonsoft.Json;
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

            if (basket == null)
                return Unit.Value;

            if (basket.Products.Count > 0)
                await RemoveProductAndDecreaseBasketTotalPrice(basket, request);

            return Unit.Value;
        }

        private async Task RemoveProductAndDecreaseBasketTotalPrice(UserBasketDto basket, RemoveProductCommand request)
        {
            var productToRemove = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

            if (productToRemove == null)
                return;

            basket.Products.Remove(productToRemove);
            basket.TotalPrice -= (productToRemove.Price * productToRemove.Quantity);

            await SerializeAndSaveBasket(basket, request.UserId);
        }

        private async Task SerializeAndSaveBasket(UserBasketDto basket, string userId)
        {
            string serializeObject = JsonConvert.SerializeObject(basket);

            await basketRedisService.SaveBasket(userId, serializeObject);
        }
    }
}

