using Delivery.Application.Commands.BaseCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery.Application.Commands
{
    public class UpdateLoadingPlaceCommand : LoadingPlaceCommand
    {
        public int Id { get; set; }
    }
}
