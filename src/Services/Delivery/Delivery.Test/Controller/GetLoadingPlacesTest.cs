using Delivery.API.Controllers;
using Delivery.Application.Mapping;
using Delivery.Application.Queries;
using Delivery.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Controller
{
    public class GetLoadingPlacesTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        private readonly DeliveryController controller;
        private readonly List<GetLoadingPlacesDto> loadingPlacesDto;

        public GetLoadingPlacesTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
            controller = new DeliveryController(mediator.Object, logger.Object);
            loadingPlacesDto = new List<GetLoadingPlacesDto> { new GetLoadingPlacesDto(), new GetLoadingPlacesDto() };
        }

        [Fact]
        public async Task GetLoadingPlaces_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetLoadingPlacesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(loadingPlacesDto));

            //Act
            var action = await controller.GetLoadingPlaces(It.IsAny<LoadingPlaceFilteringData>()) as OkObjectResult;
            var value = action.Value as List<GetLoadingPlacesDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(loadingPlacesDto.Count, value.Count);
        }
    }
}
