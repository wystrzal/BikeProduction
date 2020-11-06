using Catalog.Application.Commands.BaseCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
