using AutoMapper;
using Delivery.Application.Commands;
using Delivery.Core.Models;

namespace Delivery.Application.Mapping.Profiles
{
    class LoadingPlaceProfile : Profile
    {
        public LoadingPlaceProfile()
        {
            CreateMap<LoadingPlace, GetLoadingPlaceDto>();

            CreateMap<AddLoadingPlaceCommand, LoadingPlace>();

            CreateMap<UpdateLoadingPlaceCommand, LoadingPlace>();
        }
    }
}
