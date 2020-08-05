using Basket.Core.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

namespace Basket.Application.Commands.Handlers
{
    public class ChangeProductQuantityCommandHandler : IRequestHandler<ChangeProductQuantityCommand>
    {
        private readonly IBasketRedisService basketService;

        public ChangeProductQuantityCommandHandler(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }
        public async Task<Unit> Handle(ChangeProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasket(request.UserId);

            if (basket == null)
            {
                return Unit.Value;
            }

            var basketProduct = basket.Products.Where(x => x.Id == request.ProductId).FirstOrDefault();

            if (basketProduct == null)
            {
                return Unit.Value;
            }

            if (request.ChangeQuantityAction == ChangeQuantityAction.Plus)
            {
                basketProduct.Quantity++;
                basket.TotalPrice += basketProduct.Price;
            }
            else
            {
                basketProduct.Quantity--;
                basket.TotalPrice -= basketProduct.Price;
            }

            if (basketProduct.Quantity <= 0)
            {
                basket.Products.Remove(basketProduct);
            }

            await basketService.RemoveBasket(request.UserId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await basketService.SaveBasket(request.UserId, serializeObject);

            return Unit.Value;
        }
    }
}
