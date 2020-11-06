using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Commands.BaseCommands
{
    public abstract class ProductCommand : IRequest
    {
        [Required]
        public string Reference { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public Colors Colors { get; set; }

        [Required]
        public BikeType BikeType { get; set; }

        [Range(1, int.MaxValue)]
        public int BrandId { get; set; }
    }
}
