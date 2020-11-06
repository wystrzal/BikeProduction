using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
