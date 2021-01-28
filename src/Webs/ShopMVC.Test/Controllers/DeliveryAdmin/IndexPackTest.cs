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
    public class IndexPackTest
    {
        private readonly Mock<IDeliveryService> deliveryService;

        private readonly DeliveryController deliveryController;

        public IndexPackTest()
        {
            deliveryService = new Mock<IDeliveryService>();
            deliveryController = new DeliveryController(deliveryService.Object);
        }

        [Fact]
        public async Task IndexPack_Success()
        {
            //Arrange
            var packs = new List<PackToDelivery> { new PackToDelivery(), new PackToDelivery() };

            deliveryService.Setup(x => x.GetPacks(It.IsAny<PackFilteringData>())).Returns(Task.FromResult(packs));

            //Act
            var action = await deliveryController.IndexPack(It.IsAny<PackFilteringData>()) as ViewResult;
            var model = action.Model as PacksViewModel;

            //Assert
            Assert.Equal(packs.Count, model.Packs.Count);
        }
    }
}