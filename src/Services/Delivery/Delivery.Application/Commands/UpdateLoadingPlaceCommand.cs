using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Commands
{
    public class UpdateLoadingPlaceCommand : IRequest
    {
        public int Id { get; set; }
        public string LoadingPlaceName { get; set; }
        public int AmountOfSpace { get; set; }
    }
}
