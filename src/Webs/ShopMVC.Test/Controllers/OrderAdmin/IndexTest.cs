using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.OrderAdmin
{
    public class IndexTest
    {
        private const int pageSize = 50;

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
            var filteringData = new OrderFilteringData { Page = 1 };
            var orders = new List<Models.Order> { new Models.Order(), new Models.Order() };

            orderService.Setup(x => x.GetOrders(filteringData)).Returns(Task.FromResult(orders));

            //Act
            var action = await orderController.Index(filteringData) as ViewResult;
            var model = action.Model as OrdersViewModel;

            //Assert
            Assert.NotNull(model.FilteringData);
            Assert.Equal(orders.Count, model.Orders.Count);
            Assert.Equal(pageSize, model.PageSize);
            Assert.Equal(filteringData.Page, model.CurrentPage);
        }
    }
}