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
    public class UpdateLoadingPlaceTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;
        private readonly LoadingPlace loadingPlace;

        public UpdateLoadingPlaceTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
            loadingPlace = new LoadingPlace { Id = loadingPlaceId };
        }

        [Fact]
        public async Task UpdateLoadingPlace_Success()
        {
            //Arrange
            deliveryService.Setup(x => x.GetLoadingPlace(loadingPlaceId)).Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await deliveryController.UpdateLoadingPlace(loadingPlaceId) as ViewResult;
            var model = action.Model as PostPutLoadingPlaceViewModel;

            //Arrange
            Assert.Equal(loadingPlaceId, model.LoadingPlace.Id);
        }

        [Fact]
        public async Task UpdateLoadingPlace_PostAction_AmountOfSpaceLowerThanLoadedQuantity_Success()
        {
            //Arrange
            loadingPlace.LoadingPlaceName = "Name";
            loadingPlace.AmountOfSpace = 1;
            loadingPlace.LoadedQuantity = 2;

            deliveryService.Setup(x => x.GetLoadingPlace(loadingPlaceId)).Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await deliveryController.UpdateLoadingPlace(loadingPlace) as ViewResult;
            var model = action.Model as PostPutLoadingPlaceViewModel;

            //Arrange
            Assert.Equal(loadingPlaceId, model.LoadingPlace.Id);
        }

        [Fact]
        public async Task UpdateLoadingPlace_PostAction_NotValidModel_Success()
        {
            //Arrange
            loadingPlace.AmountOfSpace = 4;
            loadingPlace.LoadedQuantity = 2;
            deliveryController.ModelState.AddModelError("", "");

            deliveryService.Setup(x => x.GetLoadingPlace(loadingPlaceId)).Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await deliveryController.UpdateLoadingPlace(loadingPlace) as ViewResult;
            var model = action.Model as PostPutLoadingPlaceViewModel;

            //Arrange
            Assert.Equal(loadingPlaceId, model.LoadingPlace.Id);
        }

        [Fact]
        public async Task UpdateLoadingPlace_PostAction_ValidModel_Success()
        {
            //Arrange
            loadingPlace.LoadingPlaceName = "Name";
            loadingPlace.AmountOfSpace = 4;
            loadingPlace.LoadedQuantity = 2;

            deliveryService.Setup(x => x.UpdateLoadingPlace(loadingPlace));

            //Act
            var action = await deliveryController.UpdateLoadingPlace(loadingPlace) as RedirectToActionResult;

            //Arrange
            deliveryService.Verify(x => x.UpdateLoadingPlace(loadingPlace), Times.Once);
            Assert.Equal("IndexLoadingPlace", action.ActionName);
        }
    }
}