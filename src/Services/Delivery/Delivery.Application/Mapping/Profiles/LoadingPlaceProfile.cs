using AutoMapper;
using Delivery.Application.Commands;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
