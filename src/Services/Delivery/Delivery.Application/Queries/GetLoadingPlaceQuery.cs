﻿using Delivery.Application.Mapping;
using MediatR;
using System;

namespace Delivery.Application.Queries
{
    public class GetLoadingPlaceQuery : IRequest<GetLoadingPlaceDto>
    {
        public int LoadingPlaceId { get; set; }

        public GetLoadingPlaceQuery(int loadingPlaceId)
        {
            if (loadingPlaceId <= 0)
            {
                throw new ArgumentException("LoadingPlaceId must be greater than zero.");
            }

            LoadingPlaceId = loadingPlaceId;
        }
    }
}