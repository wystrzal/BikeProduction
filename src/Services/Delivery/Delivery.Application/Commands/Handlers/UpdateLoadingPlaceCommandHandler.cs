using AutoMapper;
using BikeBaseRepository;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Application.Commands.Handlers
{
    public class UpdateLoadingPlaceCommandHandler : IRequestHandler<UpdateLoadingPlaceCommand>
    {
        private readonly ILoadingPlaceRepo loadingPlaceRepo;
        private readonly IMapper mapper;

        public UpdateLoadingPlaceCommandHandler(ILoadingPlaceRepo loadingPlaceRepo, IMapper mapper)
        {
            this.loadingPlaceRepo = loadingPlaceRepo;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLoadingPlaceCommand request, CancellationToken cancellationToken)
        {
            var loadingPlace = await loadingPlaceRepo.GetById(request.Id);

            CheckIfAmountOfSpaceIsGreaterThanLoadedQuantity(request, loadingPlace);

            mapper.Map(request, loadingPlace);

            try
            {
                await loadingPlaceRepo.SaveAllAsync();
            }
            catch (ChangesNotSavedCorrectlyException)
            {
                return Unit.Value;
            }

            return Unit.Value;
        }

        private void CheckIfAmountOfSpaceIsGreaterThanLoadedQuantity(UpdateLoadingPlaceCommand request, LoadingPlace loadingPlace)
        {
            if (request.AmountOfSpace < loadingPlace.LoadedQuantity)
            {
                throw new ArgumentException("Amount of space must be greater than loaded quantity.");
            }
        }
    }
}
