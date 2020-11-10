using System;

namespace Delivery.Application.Commands
{
    public class LoadPackCommand : LoadingPlaceIdCommand
    {
        public int PackId { get; set; }

        public LoadPackCommand(int loadingPlaceId, int packId) : base(loadingPlaceId)
        {
            if (packId <= 0)
            {
                throw new ArgumentException("PackId must be greater than zero.");
            }

            PackId = packId;
        }
    }
}
