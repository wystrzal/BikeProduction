using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Commands
{
    public class AddLoadingPlaceCommand : IRequest
    {
        public string LoadingPlaceName { get; set; }
        public int AmountOfSpace { get; set; }
    }
}
