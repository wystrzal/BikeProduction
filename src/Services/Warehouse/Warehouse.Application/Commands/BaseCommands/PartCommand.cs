﻿using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Application.Commands.BaseCommand
{
    public abstract class PartCommand : IRequest
    {
        [Required]
        public string PartName { get; set; }

        [Required]
        public string Reference { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "QuantityForProduction must be greater than zero.")]
        public int QuantityForProduction { get; set; }
        public int Quantity { get; set; }
    }
}
