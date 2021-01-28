using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.DeliveryAdmin
{
    public class CreateLoadingPlaceTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public CreateLoadingPlaceTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task CreateLoadingPlace_Success()
        {
            //Act
            var action = await deliveryController.CreateLoadingPlace() as ViewResult;
            var model = action.Model as PostPutLoadingPlaceViewModel;

            //Assert
            Assert.NotNull(model);
        }

        [Fact]
        public async Task CreateLoadingPlace_PostAction_ValidModel_Success()
        {
            //Arrange
            var loadingPlace = new LoadingPlace { Id = loadingPlaceId, LoadingPlaceName = "Name", AmountOfSpace = 1 };

            //Act
            var action = await deliveryController.CreateLoadingPlace(loadingPlace) as RedirectToActionResult;

            //Assert
            deliveryService.Verify(x => x.AddLoadingPlace(loadingPlace), Times.Once);
            Assert.Equal("IndexLoadingPlace", action.ActionName);
        }

        [Fact]
        public async Task CreateLoadingPlace_PostAction_NotValidModel_Success()
        {
            //Arrange
            var loadingPlace = new LoadingPlace { Id = loadingPlaceId };
            deliveryController.ModelState.AddModelError("", "");

            //Act
            var action = await deliveryController.CreateLoadingPlace(loadingPlace) as ViewResult;
            var model = action.Model as PostPutLoadingPlaceViewModel;

            //Assert
            Assert.NotNull(model.LoadingPlace);
        }
    }
}