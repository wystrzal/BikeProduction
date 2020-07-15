using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
