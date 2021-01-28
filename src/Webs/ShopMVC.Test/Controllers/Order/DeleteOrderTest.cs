using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Order
{
    public class DeleteOrderTest
    {
        private readonly Mock<IOrderService> orderService;

        private readonly OrderController orderController;

        public DeleteOrderTest()
        {
            orderService = new Mock<IOrderService>();
            orderController = new OrderController(orderService.Object);
        }

        [Fact]
        public async Task DeleteOrder_Success()
        {
            //Act
            var action = await orderController.DeleteOrder(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            orderService.Verify(x => x.DeleteOrder(It.IsAny<int>()), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}
