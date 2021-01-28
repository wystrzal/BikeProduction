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
    public class CompleteDeliveryTest
    {
        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public CompleteDeliveryTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task CompleteDelivery_Success()
        {
            //Act
            var action = await deliveryController.CompleteDelivery(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            deliveryService.Verify(x => x.CompleteDelivery(It.IsAny<int>()), Times.Once);
            Assert.Equal("LoadingPlaceDetail", action.ActionName);
        }
    }
}