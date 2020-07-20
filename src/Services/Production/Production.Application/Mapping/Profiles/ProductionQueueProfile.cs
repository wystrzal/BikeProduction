using AutoMapper;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Application.Mapping.Profiles
{
    public class ProductionQueueProfile : Profile
    {
        public ProductionQueueProfile()
        {
            CreateMap<ProductionQueue, GetProductionQueuesDto>();
        }
    }
}
