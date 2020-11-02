using MediatR;
using System;

namespace Delivery.Application.Commands
{
    public class LoadPackCommand : IRequest
    {
        public int LoadingPlaceId { get; set; }
        public int PackId { get; set; }

        public LoadPackCommand(int loadingPlaceId, int packId)
        {
            if (loadingPlaceId <= 0)
            {
                throw new ArgumentException("LoadingPlaceId must be greater than zero.");
            }
            if (packId <= 0)
            {
                throw new ArgumentException("PackId must be greater than zero.");
            }

            LoadingPlaceId = loadingPlaceId;
            PackId = packId;
        }
    }
}
