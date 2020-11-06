using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery.Application.Commands
{
    public abstract class LoadingPlaceIdCommand : IRequest
    {
        [Required]
        public int LoadingPlaceId { get; set; }

        public LoadingPlaceIdCommand()
        {
        }

        public LoadingPlaceIdCommand(int loadingPlaceId)
        {
            if (loadingPlaceId <= 0)
            {
                throw new ArgumentException("LoadingPlaceId must be greater than zero.");
            }

            LoadingPlaceId = loadingPlaceId;
        }
    }
}
