using AutoMapper;
using Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Core.Models;

namespace Warehouse.Application.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductAddedEvent>();
        }
    }
}
