using Basket.Core.Models;
using MediatR;

namespace Basket.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        public BasketProduct Product { get; set; }
        public string UserId { get; set; }
    }
}
