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

            if (basket != null && basket.Products.Count > 0)
            {
                var productToRemove = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

                basket.TotalPrice -= (productToRemove.Price * productToRemove.Quantity);

                basket.Products.Remove(productToRemove);

                string serializeObject = JsonConvert.SerializeObject(basket);

                await basketRedisService.SaveBasket(request.UserId, serializeObject);
            }

            return Unit.Value;
        }
    }
}

