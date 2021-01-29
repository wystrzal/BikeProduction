using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Areas.Admin.Models.Dto;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.WarehouseAdmin
{
    public class AddProductPartTest
    {
        private const string reference = "1";
        private const int partId = 1;

        private readonly Mock<IWarehouseService> warehouseService;

        private readonly WarehouseController warehouseController;

        public AddProductPartTest()
        {
            warehouseService = new Mock<IWarehouseService>();
            warehouseController = new WarehouseController(warehouseService.Object);
        }

        [Fact]
        public async Task AddProductPart_Success()
        {
            //Arrange
            var addProductPartDto = new AddProductPartDto { Reference = reference, PartId = partId };

            //Act
            var action = await warehouseController.AddProductPart(addProductPartDto) as RedirectToActionResult;

            //Assert
            warehouseService.Verify(x => x.AddProductPart(reference, partId), Times.Once);
            Assert.Equal("UpdateProduct", action.ActionName);
            Assert.Equal("Product", action.ControllerName);
        }
    }
}