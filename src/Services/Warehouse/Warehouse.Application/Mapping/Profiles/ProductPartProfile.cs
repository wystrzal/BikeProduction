using AutoMapper;
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
