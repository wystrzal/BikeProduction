namespace Delivery.Application.Commands
{
    public class DeleteLoadingPlaceCommand : LoadingPlaceIdCommand
    {
        public DeleteLoadingPlaceCommand(int loadingPlaceId) : base(loadingPlaceId)
        {
        }
    }
}
