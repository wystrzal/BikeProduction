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

            if (basket == null)
                return Unit.Value;
            
            var basketProduct = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

            if (basketProduct == null)
                return Unit.Value;

            ChangeProductQuantityAndBasketTotalPrice(basketProduct, basket, request.ChangeQuantityAction);

            await SerializeAndSaveBasket(basket, request.UserId);

            return Unit.Value;
        }

        private void ChangeProductQuantityAndBasketTotalPrice(BasketProduct basketProduct, UserBasketDto basket,
            ChangeQuantityAction changeQuantityAction)
        {
            switch (changeQuantityAction)
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
        }

        private void RemoveProductIfQuantityIsLessOrEqualZero(BasketProduct basketProduct, UserBasketDto basket)
        {
            if (basketProduct.Quantity <= 0)
                basket.Products.Remove(basketProduct);
        }

        private async Task SerializeAndSaveBasket(UserBasketDto basket, string userId)
        {
            string serializeObject = JsonConvert.SerializeObject(basket);

            await basketRedisService.SaveBasket(userId, serializeObject);
        }
    }
}

