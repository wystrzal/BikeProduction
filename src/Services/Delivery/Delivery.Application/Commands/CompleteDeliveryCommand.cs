using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
