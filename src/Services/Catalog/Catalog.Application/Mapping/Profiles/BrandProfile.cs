using AutoMapper;
using Catalog.Core.Models;

namespace Catalog.Application.Mapping.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, GetBrandsDto>();
        }
    }
}
