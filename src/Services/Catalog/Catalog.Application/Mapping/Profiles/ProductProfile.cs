using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Models;

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

            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
