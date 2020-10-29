using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Core.Models;

namespace Warehouse.Application.Mapping.Profiles
{
    class ProductPartProfile : Profile
    {
        public ProductPartProfile()
        {
            CreateMap<Part, GetProductPartsDto>();
        }
    }
}
