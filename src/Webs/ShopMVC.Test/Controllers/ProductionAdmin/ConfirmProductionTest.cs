using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.ProductionAdmin
{
    public class ConfirmProductionTest
    {
        private readonly Mock<IProductionService> productionService;

        private readonly ProductionController productionController;

        public ConfirmProductionTest()
        {
            productionService = new Mock<IProductionService>();
            productionController = new ProductionController(productionService.Object);
        }

        [Fact]
        public async Task ConfirmProduction_Success()
        {
            //Act
            var action = await productionController.ConfirmProduction(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            productionService.Verify(x => x.ConfirmProduction(It.IsAny<int>()), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}