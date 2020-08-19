using AutoMapper;
using Production.Core.Models;

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
