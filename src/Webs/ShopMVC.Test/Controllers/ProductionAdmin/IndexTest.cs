using Microsoft.AspNetCore.Mvc;
using Moq;
using Production.Core.Models;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.ProductionAdmin
{
    public class IndexTest
    {
        private readonly Mock<IProductionService> productionService;

        private readonly ProductionController productionController;

        public IndexTest()
        {
            productionService = new Mock<IProductionService>();
            productionController = new ProductionController(productionService.Object);
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            var productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
            productionService.Setup(x => x.GetProductionQueues(It.IsAny<ProductionQueueFilteringData>())).Returns(Task.FromResult(productionQueues));

            //Act
            var action = await productionController.Index(It.IsAny<ProductionQueueFilteringData>()) as ViewResult;
            var model = action.Model as ProductionQueuesViewModel;

            //Assert
            Assert.Equal(productionQueues.Count, model.ProductionQueues.Count);
        }
    }
}