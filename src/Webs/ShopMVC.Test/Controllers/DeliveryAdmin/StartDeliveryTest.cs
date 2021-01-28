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
    public class StartDeliveryTest
    {
        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public StartDeliveryTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task StartDelivery_Success()
        {
            //Act
            var action = await deliveryController.StartDelivery(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            deliveryService.Verify(x => x.StartDelivery(It.IsAny<int>()), Times.Once);
            Assert.Equal("LoadingPlaceDetail", action.ActionName);
        }
    }
}