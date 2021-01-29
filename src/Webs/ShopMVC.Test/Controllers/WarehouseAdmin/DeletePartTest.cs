using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.WarehouseAdmin
{
    public class DeletePartTest
    {
        private readonly Mock<IWarehouseService> warehouseService;

        private readonly WarehouseController warehouseController;

        public DeletePartTest()
        {
            warehouseService = new Mock<IWarehouseService>();
            warehouseController = new WarehouseController(warehouseService.Object);
        }

        [Fact]
        public async Task DeletePart_Success()
        {
            //Act
            var action = await warehouseController.DeletePart(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            warehouseService.Verify(x => x.DeletePart(It.IsAny<int>()), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}