using AutoMapper;
using MediatR;
using Production.Application.Mapping;
using Production.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Production.Application.Queries.Handlers
{
    public class GetProductionQueuesQueryHandler : IRequestHandler<GetProductionQueuesQuery, IEnumerable<GetProductionQueuesDto>>
    {
        private readonly IProductionQueueRepo productionQueueRepo;
        private readonly IMapper mapper;

        public GetProductionQueuesQueryHandler(IProductionQueueRepo productionQueueRepo, IMapper mapper)
        {
            this.productionQueueRepo = productionQueueRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetProductionQueuesDto>> Handle(GetProductionQueuesQuery request, CancellationToken cancellationToken)
        {
            var productionQueues = await productionQueueRepo.GetAll();

            return mapper.Map<IEnumerable<GetProductionQueuesDto>>(productionQueues);
        }
    }
}
