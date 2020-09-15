﻿using CustomerOrder.API.Controllers;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using CustomerOrder.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Controller
{
    public class GetOrdersTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CustomerOrderController>> logger;

        public GetOrdersTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CustomerOrderController>>();
        }

        [Fact]
        public async Task GetOrders_OkObjectResult()
        {
            //Arrange
            var filteringData = new FilteringData();
            IEnumerable<GetOrdersDto> ordersDto
                = new List<GetOrdersDto> { new GetOrdersDto { OrderId = 1 }, new GetOrdersDto { OrderId = 2 } };

            mediator.Setup(x => x.Send(It.IsAny<GetOrdersQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(ordersDto));

            var controller = new CustomerOrderController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetOrders(filteringData) as OkObjectResult;
            var value = action.Value as List<GetOrdersDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, value.Count);
            Assert.Equal(1, value.Select(x => x.OrderId).First());
        }
    }
}
