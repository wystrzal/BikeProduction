using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
