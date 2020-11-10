namespace Delivery.Application.Commands
{
    public class CompleteDeliveryCommand : LoadingPlaceIdCommand
    {
        public CompleteDeliveryCommand(int loadingPlaceId) : base(loadingPlaceId)
        {
        }
    }
}
