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
    public class LoadingPlaceDetailTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public LoadingPlaceDetailTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task LoadingPlaceDetail_Success()
        {
            //Arrange
            deliveryService.Setup(x => x.GetLoadingPlace(loadingPlaceId)).Returns(Task.FromResult(new LoadingPlace { Id = loadingPlaceId }));

            //Act
            var action = await deliveryController.LoadingPlaceDetail(loadingPlaceId) as ViewResult;
            var model = action.Model as LoadingPlaceDetailViewModel;

            //Assert
            Assert.Equal(loadingPlaceId, model.LoadingPlace.Id);
        }
    }
}
