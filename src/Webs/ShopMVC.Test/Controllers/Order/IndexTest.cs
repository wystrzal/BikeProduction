using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers
{
    public class IndexTest
    {
        private readonly Mock<IOrderService> orderService;

        private readonly OrderController orderController;

        public IndexTest()
        {
            orderService = new Mock<IOrderService>();
            orderController = new OrderController(orderService.Object);
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            var orders = new List<Models.Order>();

            orderService.Setup(x => x.GetOrders(It.IsAny<OrderFilteringData>())).Returns(Task.FromResult(orders));

            //Act
            var action = await orderController.Index(It.IsAny<OrderFilteringData>()) as ViewResult;
            var value = action.Model as OrdersViewModel;

            //Assert
            Assert.Equal(orders.Count, value.Orders.Count);
        }
    }
}
