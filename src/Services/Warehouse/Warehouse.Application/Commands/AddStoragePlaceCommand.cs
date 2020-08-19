using MediatR;

namespace Warehouse.Application.Commands
{
    public class AddStoragePlaceCommand : IRequest
    {
        public string Name { get; set; }
    }
}
