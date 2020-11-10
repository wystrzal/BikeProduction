using Warehouse.Application.Commands.BaseCommand;

namespace Warehouse.Application.Commands
{
    public class AddProductPartCommand : PartIdWithReferenceCommand
    {
        public AddProductPartCommand(int partId, string reference) : base(partId, reference)
        {
        }
    }
}