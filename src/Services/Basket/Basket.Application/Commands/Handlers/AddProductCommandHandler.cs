using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
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

            await basketRedisService.RemoveBasket(request.UserId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await basketRedisService.SaveBasket(request.UserId, serializeObject);

            return Unit.Value;
        }
    }
}
