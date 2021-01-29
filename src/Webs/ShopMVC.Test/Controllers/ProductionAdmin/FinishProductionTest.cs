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
    public class FinishProductionTest
    {
        private readonly Mock<IProductionService> productionService;

        private readonly ProductionController productionController;

        public FinishProductionTest()
        {
            productionService = new Mock<IProductionService>();
            productionController = new ProductionController(productionService.Object);
        }

        [Fact]
        public async Task FinishProduction_Success()
        {
            //Act
            var action = await productionController.FinishProduction(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            productionService.Verify(x => x.FinishProduction(It.IsAny<int>()), Times.Once);
        }
    }
}