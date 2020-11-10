namespace Delivery.Application.Commands
{
    public class StartDeliveryCommand : LoadingPlaceIdCommand
    {
        public StartDeliveryCommand(int loadingPlaceId) : base(loadingPlaceId)
        {
        }
    }
}
