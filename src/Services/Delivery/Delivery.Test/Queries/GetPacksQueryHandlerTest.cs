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
    public class GetPacksQueryHandlerTest
    {
        private readonly Mock<ISearchPacksService> searchPacksService;
        private readonly Mock<IMapper> mapper;

        private readonly OrderFilteringData filteringData;
        private readonly GetPacksQuery query;
        private readonly GetPacksQueryHandler queryHandler;
        private readonly List<PackToDelivery> packsToDelivery;
        private readonly List<GetPacksDto> packsDto;

        public GetPacksQueryHandlerTest()
        {
            searchPacksService = new Mock<ISearchPacksService>();
            mapper = new Mock<IMapper>();
            filteringData = new OrderFilteringData();
            query = new GetPacksQuery(filteringData);
            queryHandler = new GetPacksQueryHandler(searchPacksService.Object, mapper.Object);
            packsToDelivery = new List<PackToDelivery>() { new PackToDelivery(), new PackToDelivery() };
            packsDto = new List<GetPacksDto> { new GetPacksDto(), new GetPacksDto() };
        }

        [Fact]
        public async Task GetPacksQueryHandler_Success()
        {
            //Arrange
            searchPacksService.Setup(x => x.SearchPacks(filteringData)).Returns(Task.FromResult(packsToDelivery));
            mapper.Setup(x => x.Map<List<GetPacksDto>>(packsToDelivery)).Returns(packsDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(packsDto.Count, action.Count);
        }
    }
}
