using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Commands
{
    public class DeleteLoadingPlaceCommand : IRequest
    {
        public int LoadingPlaceId { get; set; }

        public DeleteLoadingPlaceCommand(int loadingPlaceId)
        {
            if (loadingPlaceId <= 0)
            {
                throw new ArgumentException("LoadingPlaceId must be greater than zero.");
            }

            LoadingPlaceId = loadingPlaceId;
        }
    }
}
