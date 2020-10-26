using Basket.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        [Required]
        public BasketProduct Product { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
