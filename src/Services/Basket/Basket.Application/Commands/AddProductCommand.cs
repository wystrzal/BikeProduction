using Basket.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class AddProductCommand : UserIdCommand
    {
        [Required]
        public BasketProduct Product { get; set; }
    }
}
