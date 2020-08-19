using MediatR;

namespace Delivery.Application.Commands
{
    public class CompleteDeliveryCommand : IRequest
    {
        public int LoadingPlaceId { get; set; }

        public CompleteDeliveryCommand(int loadingPlaceId)
        {
            LoadingPlaceId = loadingPlaceId;
        }
    }
}
