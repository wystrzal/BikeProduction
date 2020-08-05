using Basket.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Handlers
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommand>
    {
        private readonly IBasketRedisService basketService;

        public ClearBasketCommandHandler(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<Unit> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            await basketService.RemoveBasket(request.UserId);

            return Unit.Value;
        }
    }
}
