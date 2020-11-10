using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using CustomerOrder.Application.Queries.Handlers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Queries
{
    public class GetOrdersQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchOrderService> searchOrderService;

        private readonly GetOrdersQuery query;
        private readonly GetOrdersQueryHandler queryHandler;
        private readonly List<Order> orders;
        private readonly List<GetOrdersDto> ordersDto;

        public GetOrdersQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            searchOrderService = new Mock<ISearchOrderService>();
            query = new GetOrdersQuery(It.IsAny<FilteringData>());
            queryHandler = new GetOrdersQueryHandler(searchOrderService.Object, mapper.Object);
            orders = new List<Order> { new Order(), new Order() };
            ordersDto = new List<GetOrdersDto> { new GetOrdersDto(), new GetOrdersDto() };
        }

        [Fact]
        public async Task GetOrdersQueryHandler_Success()
        {
            //Arrange
            searchOrderService.Setup(x => x.GetOrders(It.IsAny<FilteringData>())).Returns(Task.FromResult(orders));

            mapper.Setup(x => x.Map<List<GetOrdersDto>>(orders)).Returns(ordersDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(ordersDto.Count, action.Count());
        }
    }
}
