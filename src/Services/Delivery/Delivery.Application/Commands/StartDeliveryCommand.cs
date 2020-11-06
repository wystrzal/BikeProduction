using MediatR;
using System;

namespace Delivery.Application.Commands
{
    public class StartDeliveryCommand : BaseLoadingPlaceCommand
    {
        public StartDeliveryCommand(int loadingPlaceId) : base(loadingPlaceId)
        {
        }
    }
}
