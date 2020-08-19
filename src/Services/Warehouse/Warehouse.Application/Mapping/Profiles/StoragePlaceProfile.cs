using AutoMapper;
using Warehouse.Application.Commands;
using Warehouse.Core.Models;

namespace Warehouse.Application.Mapping
{
    public class StoragePlaceProfile : Profile
    {
        public StoragePlaceProfile()
        {
            CreateMap<AddStoragePlaceCommand, StoragePlace>();

            CreateMap<StoragePlace, GetStoragePlacesDto>()
                .ForMember(dest => dest.ItsFree, opt =>
                opt.MapFrom(src => src.Part != null));
        }
    }
}
