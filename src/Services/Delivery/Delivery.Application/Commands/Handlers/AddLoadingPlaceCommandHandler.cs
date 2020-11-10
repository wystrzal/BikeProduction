using AutoMapper;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Application.Commands.Handlers
{
    public class AddLoadingPlaceCommandHandler : IRequestHandler<AddLoadingPlaceCommand>
    {
        private readonly ILoadingPlaceRepo loadingPlaceRepo;
        private readonly IMapper mapper;

        public AddLoadingPlaceCommandHandler(ILoadingPlaceRepo loadingPlaceRepo, IMapper mapper)
        {
            this.loadingPlaceRepo = loadingPlaceRepo;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(AddLoadingPlaceCommand request, CancellationToken cancellationToken)
        {
            var loadingPlaceToAdd = mapper.Map<LoadingPlace>(request);
            loadingPlaceToAdd.LoadingPlaceStatus = LoadingPlaceStatus.Waiting_For_Loading;

            await AddLoadingPlace(loadingPlaceToAdd);

            return Unit.Value;
        }

        private async Task AddLoadingPlace(LoadingPlace loadingPlace)
        {
            loadingPlaceRepo.Add(loadingPlace);
            await loadingPlaceRepo.SaveAllAsync();
        }
    }
}
