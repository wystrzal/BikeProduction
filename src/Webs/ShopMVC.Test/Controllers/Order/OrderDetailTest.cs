using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Order
{
    public class OrderDetailTest
    {
        private readonly Mock<IOrderService> orderService;

        private readonly OrderController orderController;

        public OrderDetailTest()
        {
            orderService = new Mock<IOrderService>();
            orderController = new OrderController(orderService.Object);
        }

        [Fact]
        public async Task OrderDetail_Success()
        {
            //Arrange
            var viewModel = new OrderDetailViewModel { CustomerFirstName = "Name" };

            orderService.Setup(x => x.GetOrderDetail(It.IsAny<int>()))
                .Returns(Task.FromResult(viewModel));

            //Act
            var action = await orderController.OrderDetail(It.IsAny<int>()) as ViewResult;
            var model = action.Model as OrderDetailViewModel;

            //Assert
            Assert.Equal(viewModel.CustomerFirstName, model.CustomerFirstName);
        }
    }
}