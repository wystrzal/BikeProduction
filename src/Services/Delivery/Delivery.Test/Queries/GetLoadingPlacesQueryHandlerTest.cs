using AutoMapper;
using Delivery.Application.Mapping;
using Delivery.Application.Queries;
using Delivery.Application.Queries.Handlers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Queries
{
    public class GetLoadingPlacesQueryHandlerTest
    {
        private readonly Mock<ISearchLoadingPlacesService> searchLoadingPlacesService;
        private readonly Mock<IMapper> mapper;

        private readonly LoadingPlaceFilteringData filteringData;
        private readonly GetLoadingPlacesQuery query;
        private readonly GetLoadingPlacesQueryHandler queryHandler;
        private readonly List<LoadingPlace> loadingPlaces;
        private readonly List<GetLoadingPlacesDto> loadingPlacesDto;

        public GetLoadingPlacesQueryHandlerTest()
        {
            searchLoadingPlacesService = new Mock<ISearchLoadingPlacesService>();
            mapper = new Mock<IMapper>();
            filteringData = new LoadingPlaceFilteringData();
            query = new GetLoadingPlacesQuery(filteringData);
            queryHandler = new GetLoadingPlacesQueryHandler(searchLoadingPlacesService.Object, mapper.Object);
            loadingPlaces = new List<LoadingPlace> { new LoadingPlace(), new LoadingPlace() };
            loadingPlacesDto = new List<GetLoadingPlacesDto> { new GetLoadingPlacesDto(), new GetLoadingPlacesDto() };
        }

        [Fact]
        public async Task GetLoadingPlacesQueryHandler_Success()
        {
            //Arrange
            searchLoadingPlacesService.Setup(x => x.SearchLoadingPlaces(filteringData)).Returns(Task.FromResult(loadingPlaces));
            mapper.Setup(x => x.Map<List<GetLoadingPlacesDto>>(loadingPlaces)).Returns(loadingPlacesDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(loadingPlacesDto.Count, action.Count);
        }
    }
}
