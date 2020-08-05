using Basket.Core.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Handlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
    {
        private readonly IBasketRedisService basketService;

        public RemoveProductCommandHandler(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasket(request.UserId);

            if (basket != null && basket.Products.Count > 0)
            {
                var productToRemove = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

                basket.TotalPrice -= (productToRemove.Price * productToRemove.Quantity);

                basket.Products.Remove(productToRemove);

                await basketService.RemoveBasket(request.UserId);

                string serializeObject = JsonConvert.SerializeObject(basket);

                await basketService.SaveBasket(request.UserId, serializeObject);
            }

            return Unit.Value;
        }
    }
}
