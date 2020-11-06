using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery.Application.Commands
{
    public abstract class BaseLoadingPlaceCommand : IRequest
    {
        [Required]
        public int LoadingPlaceId { get; set; }

        public BaseLoadingPlaceCommand()
        {
        }

        public BaseLoadingPlaceCommand(int loadingPlaceId)
        {
            if (loadingPlaceId <= 0)
            {
                throw new ArgumentException("LoadingPlaceId must be greater than zero.");
            }

            LoadingPlaceId = loadingPlaceId;
        }
    }
}
