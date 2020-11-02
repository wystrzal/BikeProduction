using MediatR;
using System;

namespace Delivery.Application.Commands
{
    public class StartDeliveryCommand : IRequest
    {
        public int LoadingPlaceId { get; set; }

        public StartDeliveryCommand(int loadingPlaceId)
        {
            if (loadingPlaceId <= 0)
            {
                throw new ArgumentException("LoadingPlaceId must be greater than zero.");
            }

            LoadingPlaceId = loadingPlaceId;
        }
    }
}
