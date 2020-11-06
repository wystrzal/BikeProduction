using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Commands
{
    public class DeleteLoadingPlaceCommand : LoadingPlaceIdCommand
    {
        public DeleteLoadingPlaceCommand(int loadingPlaceId) : base(loadingPlaceId)
        {
        }
    }
}
