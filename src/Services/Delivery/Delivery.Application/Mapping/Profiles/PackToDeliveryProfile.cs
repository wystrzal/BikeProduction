using AutoMapper;
using Delivery.Application.Queries;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
