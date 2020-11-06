using Basket.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class AddProductCommand : BaseCommand
    {
        [Required]
        public BasketProduct Product { get; set; }
    }
}
