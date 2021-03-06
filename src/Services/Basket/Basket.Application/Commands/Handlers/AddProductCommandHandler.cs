﻿using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IBasketRedisService basketRedisService;

        public AddProductCommandHandler(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRedisService.GetBasket(request.UserId);

            var basketProduct = basket.Products.Where(x => x.Id == request.Product.Id).FirstOrDefault();

            await AddProductToBasketAndIncreaseBasketTotalPrice(basketProduct, basket, request);

            return Unit.Value;
        }

        private async Task AddProductToBasketAndIncreaseBasketTotalPrice(BasketProduct basketProduct,
            UserBasketDto basket, AddProductCommand request)
        {
            if (basketProduct != null)
            {
                basketProduct.Quantity += request.Product.Quantity;
            }
            else
            {
                basket.Products.Add(request.Product);
            }

            basket.TotalPrice += (request.Product.Price * request.Product.Quantity);

            await basketRedisService.SaveBasket(request.UserId, basket);
        }
    }
}

