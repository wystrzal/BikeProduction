using MediatR;
using System;

namespace Delivery.Application.Commands
{
    public class CompleteDeliveryCommand : BaseLoadingPlaceCommand
    {
        public CompleteDeliveryCommand(int loadingPlaceId) : base(loadingPlaceId)
        {
        }
    }
}
