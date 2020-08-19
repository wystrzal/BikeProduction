using Basket.Core.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

namespace Basket.Application.Commands.Handlers
{
    public class ChangeProductQuantityCommandHandler : IRequestHandler<ChangeProductQuantityCommand>
    {
        private readonly IBasketRedisService basketRedisService;

        public ChangeProductQuantityCommandHandler(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }
        public async Task<Unit> Handle(ChangeProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRedisService.GetBasket(request.UserId);

            if (basket == null)
            {
                return Unit.Value;
            }

            var basketProduct = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

            if (basketProduct == null)
            {
                return Unit.Value;
            }

            switch (request.ChangeQuantityAction)
            {
                case ChangeQuantityAction.Plus:
                    basketProduct.Quantity++;
                    basket.TotalPrice += basketProduct.Price;
                    break;
                case ChangeQuantityAction.Minus:
                    basketProduct.Quantity--;
                    basket.TotalPrice -= basketProduct.Price;
                    break;
                default:
                    break;
            }

            if (basketProduct.Quantity <= 0)
            {
                basket.Products.Remove(basketProduct);
            }

            await basketRedisService.RemoveBasket(request.UserId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await basketRedisService.SaveBasket(request.UserId, serializeObject);

            return Unit.Value;
        }
    }
}
