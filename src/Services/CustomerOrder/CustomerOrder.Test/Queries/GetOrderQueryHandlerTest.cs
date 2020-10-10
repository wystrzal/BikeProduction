using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using CustomerOrder.Application.Queries.Handlers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Queries
{
    public class GetOrderQueryHandlerTest
    {
        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<IMapper> mapper;

        public GetOrderQueryHandlerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetOrderQueryHandler_Success()
        {
            //Arrange
            var id = 1;
            var order = new Order { OrderId = id };
            var orderDto = new GetOrderDto { OrderId = id };

            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>())).Returns(Task.FromResult(order));

            mapper.Setup(x => x.Map<GetOrderDto>(order)).Returns(orderDto);

            var queryHandler = new GetOrderQueryHandler(orderRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(id, action.OrderId);
        }
    }
}
