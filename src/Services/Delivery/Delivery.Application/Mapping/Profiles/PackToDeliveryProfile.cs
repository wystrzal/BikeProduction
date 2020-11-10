using AutoMapper;
using Delivery.Core.Models;

namespace Delivery.Application.Mapping.Profiles
{
    class PackToDeliveryProfile : Profile
    {
        public PackToDeliveryProfile()
        {
            CreateMap<PackToDelivery, GetPacksDto>();

            CreateMap<PackToDelivery, GetPackDto>()
                .ForMember(dest => dest.LoadingPlaceId,
                opt => opt.MapFrom(src => src.LoadingPlace.Id));

            CreateMap<LoadingPlace, GetLoadingPlacesDto>();
        }
    }
}
