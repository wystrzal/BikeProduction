using MediatR;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public Colors Colors { get; set; }
    }
}
