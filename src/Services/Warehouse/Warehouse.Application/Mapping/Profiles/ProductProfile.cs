using AutoMapper;
using Common.Application.Messaging;
using Warehouse.Core.Models;

namespace Warehouse.Application.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddedEvent, Product>();
        }
    }
}
