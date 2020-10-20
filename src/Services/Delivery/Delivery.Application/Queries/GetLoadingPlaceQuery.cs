using Delivery.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Application.Queries
{
    public class GetLoadingPlaceQuery : IRequest<GetLoadingPlaceDto>
    {
        public int LoadingPlaceId { get; set; }

        public GetLoadingPlaceQuery(int loadingPlaceId)
        {
            LoadingPlaceId = loadingPlaceId;
        }
    }
}