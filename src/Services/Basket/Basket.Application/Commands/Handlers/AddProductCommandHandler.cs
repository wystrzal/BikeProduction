using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IBasketRedisService basketService;

        public AddProductCommandHandler(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasket(request.UserId);

            if (basket == null)
            {
                basket = new UserBasketDto()
                {
                    Products = new List<Core.Models.BasketProduct>(),
                    TotalPrice = 0,
                    UserId = request.UserId
                };
            }

            var basketProduct = basket.Products.Where(x => x.Id == request.Product.Id).FirstOrDefault();

            if (basketProduct != null)
            {
                basketProduct.Quantity += request.Product.Quantity;
            }
            else
            {
                basket.Products.Add(request.Product);
            }

            basket.TotalPrice += (request.Product.Price * request.Product.Quantity);

            await basketService.RemoveBasket(request.UserId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await basketService.SaveBasket(request.UserId, serializeObject);

            return Unit.Value;
        }
    }
}
