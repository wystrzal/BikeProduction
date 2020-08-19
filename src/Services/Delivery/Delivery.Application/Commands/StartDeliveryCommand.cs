using MediatR;

namespace Delivery.Application.Commands
{
    public class StartDeliveryCommand : IRequest
    {
        public int LoadingPlaceId { get; set; }

        public StartDeliveryCommand(int loadingPlaceId)
        {
            LoadingPlaceId = loadingPlaceId;
        }
    }
}
