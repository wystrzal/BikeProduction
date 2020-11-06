using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Application.Commands.BaseCommand;

namespace Warehouse.Application.Commands
{
    public class UpdatePartCommand : PartCommand
    {
        [Range(1, int.MaxValue, ErrorMessage = "ID must be greater than zero.")]
        public int Id { get; set; }
    }
}
