using MediatR;

namespace Warehouse.Application.Commands
{
    public class AddPartCommand : IRequest
    {
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int QuantityForProduction { get; set; }
        public int Quantity { get; set; }
    }
}
