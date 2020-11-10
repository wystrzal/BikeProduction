using Catalog.Application.Commands.BaseCommands;
using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
