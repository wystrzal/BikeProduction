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
    public class IndexLoadingPlaceTest
    {
        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public IndexLoadingPlaceTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task IndexLoadingPlace_Success()
        {
            //Arrange
            var loadingPlaces = new List<LoadingPlace> { new LoadingPlace(), new LoadingPlace() };

            deliveryService.Setup(x => x.GetLoadingPlaces(It.IsAny<LoadingPlaceFilteringData>())).Returns(Task.FromResult(loadingPlaces));

            //Act
            var action = await deliveryController.IndexLoadingPlace(It.IsAny<LoadingPlaceFilteringData>()) as ViewResult;
            var model = action.Model as LoadingPlacesViewModel;

            //Assert
            Assert.Equal(loadingPlaces.Count, model.LoadingPlaces.Count);
        }
    }
}
