using Basket.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Handlers
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommand>
    {
        private readonly IBasketRedisService basketRedisService;

        public ClearBasketCommandHandler(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }

        public async Task<Unit> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            await basketRedisService.RemoveBasket(request.UserId);

            return Unit.Value;
        }
    }
}
