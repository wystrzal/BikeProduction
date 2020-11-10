using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Application.Commands.BaseCommands
{
    public abstract class LoadingPlaceCommand : IRequest
    {
        [Required]
        public string LoadingPlaceName { get; set; }

        [Range(1, int.MaxValue)]
        public int AmountOfSpace { get; set; }
    }
}
