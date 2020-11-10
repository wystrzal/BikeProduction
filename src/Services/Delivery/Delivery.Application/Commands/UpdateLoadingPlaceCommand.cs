using Delivery.Application.Commands.BaseCommands;

namespace Delivery.Application.Commands
{
    public class UpdateLoadingPlaceCommand : LoadingPlaceCommand
    {
        public int Id { get; set; }
    }
}
