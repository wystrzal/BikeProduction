using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
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

            var basketProduct = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

            if (basketProduct == null)
            {
                return Unit.Value;
            }

            await ChangeProductQuantityAndBasketTotalPrice(basketProduct, basket, request);

            return Unit.Value;
        }

        private async Task ChangeProductQuantityAndBasketTotalPrice(BasketProduct basketProduct, UserBasketDto basket,
            ChangeProductQuantityCommand request)
        {
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

            RemoveProductIfQuantityIsLessOrEqualZero(basketProduct, basket);

            await basketRedisService.SaveBasket(request.UserId, basket);
        }

        private void RemoveProductIfQuantityIsLessOrEqualZero(BasketProduct basketProduct, UserBasketDto basket)
        {
            if (basketProduct.Quantity <= 0)
            {
                basket.Products.Remove(basketProduct);
            }
        }
    }
}

