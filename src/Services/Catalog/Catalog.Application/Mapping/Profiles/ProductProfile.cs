using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductCommand, Product>();

            CreateMap<Product, GetProductsDto>();

            CreateMap<Product, GetHomeProductsDto>();

            CreateMap<Product, GetProductDto>()
                .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name));
        }
    }
}
