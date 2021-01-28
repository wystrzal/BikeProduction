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
    public class PackDetailTest
    {
        private const int packId = 1;

        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public PackDetailTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task PackDetail_Success()
        {
            //Arrange
            var loadingPlaces = new List<LoadingPlace> { new LoadingPlace(), new LoadingPlace() };

            deliveryService.Setup(x => x.GetPack(packId)).Returns(Task.FromResult(new PackToDelivery { Id = packId }));
            deliveryService.Setup(x => x.GetLoadingPlaces(It.IsAny<LoadingPlaceFilteringData>())).Returns(Task.FromResult(loadingPlaces));

            //Act
            var action = await deliveryController.PackDetail(packId) as ViewResult;
            var model = action.Model as PackDetailViewModel;

            //Assert
            Assert.Equal(packId, model.PackToDelivery.Id);
            Assert.Equal(loadingPlaces.Count, model.LoadingPlaces.Count);
        }
    }
}
