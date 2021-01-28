using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.DeliveryAdmin
{
    public class LoadPackTest
    {
        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public LoadPackTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task LoadPack_Success()
        {
            //Act
            var action = await deliveryController.LoadPack(It.IsAny<int>(), It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            deliveryService.Verify(x => x.LoadPack(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            Assert.Equal("PackDetail", action.ActionName);
        }
    }
}