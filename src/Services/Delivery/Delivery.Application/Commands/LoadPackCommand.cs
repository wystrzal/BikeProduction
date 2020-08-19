using MediatR;

namespace Delivery.Application.Commands
{
    public class LoadPackCommand : IRequest
    {
        public int LoadingPlaceId { get; set; }
        public int PackId { get; set; }

        public LoadPackCommand(int loadingPlaceId, int packId)
        {
            LoadingPlaceId = loadingPlaceId;
            PackId = packId;
        }
    }
}
