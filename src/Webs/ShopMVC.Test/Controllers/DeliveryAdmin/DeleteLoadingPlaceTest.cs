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
    public class DeleteLoadingPlaceTest
    {
        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public DeleteLoadingPlaceTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task DeleteLoadingPlace_Success()
        {
            //Act
            var action = await deliveryController.DeleteLoadingPlace(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            deliveryService.Verify(x => x.DeleteLoadingPlace(It.IsAny<int>()), Times.Once);
            Assert.Equal("IndexLoadingPlace", action.ActionName);
        }
    }
}